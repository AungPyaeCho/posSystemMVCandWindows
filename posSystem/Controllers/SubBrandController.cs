using Microsoft.AspNetCore.Mvc;
using posSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace posSystem.Controllers
{
    public class SubBrandController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExcelService _fileUploadService;
        private readonly ILogger<SubBrandController> _logger;

        public SubBrandController(AppDbContext appDbContext, IExcelService fileUploadService, ILogger<SubBrandController> logger)
        {
            _appDbContext = appDbContext;
            _fileUploadService = fileUploadService;
            _logger = logger;
        }

        [HttpGet]
        [ActionName("MassUpload")]
        public IActionResult MassUpload()
        {
            try
            {
                bool hasData = _appDbContext.SubBrands.Any();
                ViewBag.HasData = hasData;
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while accessing MassUpload view.");
                return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier, ErrorMessage = "An error occurred while accessing the Mass Upload view." });
            }
        }

        [HttpPost]
        [ActionName("MassUpload")]
        public IActionResult MassUpload(IFormFile file)
        {
            var rspModel = new MsgResopnseModel();

            if (file == null || file.Length == 0)
            {
                rspModel = new MsgResopnseModel
                {
                    IsSuccess = false,
                    responeMessage = "No file selected or file is empty."
                };
                return Json(rspModel);
            }

            try
            {
                var subBrands = _fileUploadService.ReadFromExcel<SubBrandModel>(file);

                foreach (var items in subBrands)
                {
                    var brandItem = _appDbContext.Brands.FirstOrDefault(c => c.brandCode == items.brandCode);
                    if (brandItem == null)
                    {
                        _logger.LogWarning($"Brand with code {items.brandCode} not found for SubBrand {items.subBrandCode}.");
                        continue;
                    }

                    items.brandId = brandItem.brandId;
                    items.subBrandCreateAt = DateTime.Now.ToString();
                    _appDbContext.SubBrands.Add(items);
                }

                int result = _appDbContext.SaveChanges();
                rspModel = new MsgResopnseModel
                {
                    IsSuccess = result > 0,
                    responeMessage = result > 0 ? "Sub-Brands saved successfully!" : "Failed to save sub-brands."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the MassUpload request.");
                rspModel = new MsgResopnseModel
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
            }

            return Json(rspModel);
        }

        [HttpPost]
        [ActionName("MassUpdate")]
        public async Task<IActionResult> MassUpdate(IFormFile file)
        {
            MsgResopnseModel rspModel = new MsgResopnseModel();

            if (file == null || file.Length == 0)
            {
                rspModel = new MsgResopnseModel
                {
                    IsSuccess = false,
                    responeMessage = "No file uploaded. Please select an Excel file to update categories."
                };
                return Json(rspModel);
            }

            if (!file.FileName.EndsWith(".xlsx") && !file.FileName.EndsWith(".xls"))
            {
                rspModel = new MsgResopnseModel
                {
                    IsSuccess = false,
                    responeMessage = "Invalid file format. Please upload a valid Excel file."
                };
                return Json(rspModel);
            }

            try
            {
                var updateSubBrands = _fileUploadService.ReadFromExcel<SubBrandModel>(file);

                foreach (var items in updateSubBrands)
                {
                    var existingBrands = _appDbContext.SubBrands
                        .FirstOrDefault(c => c.subBrandCode == items.subBrandCode);

                    if (existingBrands == null)
                    {
                        _logger.LogWarning($"SubBrand with code {items.subBrandCode} not found.");
                        continue;
                    }

                    var brandItem = _appDbContext.Brands.FirstOrDefault(c => c.brandCode == items.brandCode);
                    if (brandItem == null)
                    {
                        _logger.LogWarning($"Brand with code {items.brandCode} not found for SubBrand {items.subBrandCode}.");
                        continue;
                    }

                    existingBrands.subBrandName = items.subBrandName;
                    existingBrands.brandId = brandItem.brandId;
                    existingBrands.subBrandCode = items.subBrandCode;
                    existingBrands.brandCode = items.brandCode;
                    existingBrands.subBrandUpdateAt = DateTime.Now.ToString();
                    existingBrands.subBrandUpdateCount ??= 0;
                    existingBrands.subBrandUpdateCount++;
                }

                int result = await _appDbContext.SaveChangesAsync();
                string message = result > 0 ? "Sub-Brands updated successfully!" : "Failed to update sub-brands.";

                rspModel = new MsgResopnseModel
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the MassUpdate request.");
                rspModel = new MsgResopnseModel
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
            }

            return Json(rspModel);
        }

        private (List<SubBrandModel>, int) GetSorted(int pageNo, int pageSize, string sortField, string sortOrder)
        {
            try
            {
                int rowCount = _appDbContext.SubBrands.Count();
                int pageCount = rowCount / pageSize;

                if (rowCount % pageSize > 0)
                    pageCount++;

                bool ascending = sortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase);

                IQueryable<SubBrandModel> query = _appDbContext.SubBrands.AsQueryable();

                switch (sortField.ToLower())
                {
                    case "subcatname":
                        query = ascending ? query.OrderBy(p => p.subBrandName) : query.OrderByDescending(p => p.subBrandName);
                        break;
                    case "subcatcode":
                        query = ascending ? query.OrderBy(p => p.subBrandCode) : query.OrderByDescending(p => p.subBrandCode);
                        break;
                    default:
                        query = ascending ? query.OrderBy(p => p.subBId) : query.OrderByDescending(p => p.subBId);
                        break;
                }

                List<SubBrandModel> list = query
                    .Include(s => s.Brand)
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .Select(s => new SubBrandModel
                    {
                        subBId = s.subBId,
                        subBrandName = s.subBrandName,
                        subBrandCode = s.subBrandCode,
                        subBrandCreateAt = s.subBrandCreateAt,
                        subBrandUpdateAt = s.subBrandUpdateAt,
                        subBrandUpdateCount = s.subBrandUpdateCount,
                        brandName = _appDbContext.Brands
                            .Where(c => c.brandCode == s.brandCode || c.brandId == s.brandId)
                            .Select(c => c.brandName)
                            .FirstOrDefault(),
                    })
                    .ToList();
                return (list, pageCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while sorting SubBrands.");
                throw; // Re-throw the exception to be handled at a higher level
            }
        }

        [ActionName("Index")]
        public IActionResult SubBrandIndex(int pageNo = 1, int pageSize = 10, string sortField = "", string sortOrder = "asc")
        {
            try
            {
                var (list, pageCount) = GetSorted(pageNo, pageSize, sortField, sortOrder);

                SubBrandResponseModel response = new()
                {
                    subBrandData = list,
                    pageSize = pageSize,
                    pageCount = pageCount,
                    pageNo = pageNo,
                    sortField = sortField,
                    sortOrder = sortOrder
                };

                return View("SubBrandIndex", response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while accessing SubBrandIndex view.");
                ModelState.AddModelError("", "An error occurred while accessing the SubBrand Index view.");
                return View("SubBrandIndex");
            }
        }

        [ActionName("Create")]
        public IActionResult SubBrandCreate()
        {
            try
            {
                var brands = _appDbContext.Brands.ToList();
                ViewBag.Brands = brands;
                return View("SubBrandCreate");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while accessing SubBrandCreate view.");
                return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier, ErrorMessage = "An error occurred while accessing the SubBrand Create view." });
            }
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> SubBrandCreate(SubBrandModel subBrand)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("SubBrandCreate", subBrand);
                }

                _appDbContext.SubBrands.Add(subBrand);
                await _appDbContext.SaveChangesAsync();
                TempData["SuccessMessage"] = "SubBrand created successfully!";
                return RedirectToAction("SubBrandIndex");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating SubBrand.");
                ModelState.AddModelError("", "An error occurred while creating SubBrand.");
                return View("SubBrandCreate", subBrand);
            }
        }

        [ActionName("Edit")]
        public IActionResult SubBrandEdit(int id)
        {
            try
            {
                var subBrand = _appDbContext.SubBrands.Find(id);

                if (subBrand == null)
                {
                    _logger.LogWarning($"SubBrand with id {id} not found.");
                    return NotFound();
                }

                var brands = _appDbContext.Brands.ToList();
                ViewBag.Brands = brands;
                return View("SubBrandEdit", subBrand);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while accessing SubBrandEdit view.");
                return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier, ErrorMessage = "An error occurred while accessing the SubBrand Edit view." });
            }
        }

        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> SubBrandEdit(SubBrandModel subBrand)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("SubBrandEdit", subBrand);
                }

                _appDbContext.SubBrands.Update(subBrand);
                await _appDbContext.SaveChangesAsync();
                TempData["SuccessMessage"] = "SubBrand updated successfully!";
                return RedirectToAction("SubBrandIndex");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating SubBrand.");
                ModelState.AddModelError("", "An error occurred while updating SubBrand.");
                return View("SubBrandEdit", subBrand);
            }
        }

        [ActionName("Delete")]
        public IActionResult SubBrandDelete(int id)
        {
            try
            {
                var subBrand = _appDbContext.SubBrands.Find(id);

                if (subBrand == null)
                {
                    _logger.LogWarning($"SubBrand with id {id} not found.");
                    return NotFound();
                }

                _appDbContext.SubBrands.Remove(subBrand);
                _appDbContext.SaveChanges();
                TempData["SuccessMessage"] = "SubBrand deleted successfully!";
                return RedirectToAction("SubBrandIndex");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting SubBrand.");
                return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier, ErrorMessage = "An error occurred while deleting the SubBrand." });
            }
        }
    }
}
