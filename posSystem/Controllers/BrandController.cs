using Microsoft.AspNetCore.Mvc;
using Serilog;
using posSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace posSystem.Controllers
{
    public class BrandController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExcelService _fileUploadService;
        private readonly ILogger<BrandController> _logger;

        // Constructor to set up the controller with database context, file upload service, and logger
        public BrandController(AppDbContext appDbContext, IExcelService fileUploadService, ILogger<BrandController> logger)
        {
            _appDbContext = appDbContext;
            _fileUploadService = fileUploadService;
            _logger = logger;
        }

        // GET action to show the mass upload form
        [HttpGet]
        [ActionName("MassUpload")]
        public IActionResult MassUpload()
        {
            try
            {
                bool hasData = _appDbContext.Brands.Any();
                ViewBag.HasData = hasData;
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while loading MassUpload view.");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST action to handle the mass upload of brands from an Excel file
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
                var brands = _fileUploadService.ReadFromExcel<BrandModel>(file);

                foreach (var item in brands)
                {
                    item.brandCreatedAt = DateTime.Now.ToString();
                    _appDbContext.Brands.Add(item);
                }

                int result = _appDbContext.SaveChanges();

                rspModel = new MsgResopnseModel
                {
                    IsSuccess = result > 0,
                    responeMessage = result > 0 ? "Brands saved successfully!" : "Failed to save brands."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing mass upload.");
                rspModel = new MsgResopnseModel
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
            }

            return Json(rspModel);
        }

        // POST action to update existing brands from an Excel file
        [HttpPost]
        [ActionName("MassUpdate")]
        public async Task<IActionResult> MassUpdate(IFormFile file)
        {
            MsgResopnseModel rspModel = new MsgResopnseModel();

            if (file == null || file.Length == 0)
            {
                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = "No file uploaded. Please select an Excel file to update brands."
                };
                return Json(rspModel);
            }

            if (!file.FileName.EndsWith(".xlsx") && !file.FileName.EndsWith(".xls"))
            {
                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = "Invalid file format. Please upload a valid Excel file."
                };
                return Json(rspModel);
            }

            try
            {
                var updatedBrands = _fileUploadService.ReadFromExcel<BrandModel>(file);

                foreach (var item in updatedBrands)
                {
                    var existingBrand = _appDbContext.Brands
                        .FirstOrDefault(c => c.brandCode == item.brandCode);

                    if (existingBrand != null)
                    {
                        existingBrand.brandName = item.brandName;
                        existingBrand.brandDescription = item.brandDescription;
                        existingBrand.brandUpdatedAt = DateTime.Now.ToString();
                        existingBrand.brandUpdateCount ??= 0;
                        existingBrand.brandUpdateCount++;
                    }
                }

                int result = await _appDbContext.SaveChangesAsync();
                string message = result > 0 ? "Brands updated successfully!" : "Failed to update brands.";

                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating brands from Excel.");
                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
            }

            return Json(rspModel);
        }

        // Helper method to sort and paginate brands
        private (List<BrandModel>, int) GetSorted(int pageNo, int pageSize, string sortField, string sortOrder)
        {
            int rowCount = _appDbContext.Brands.Count();
            int pageCount = rowCount / pageSize;

            if (rowCount % pageSize > 0)
                pageCount++;

            bool ascending = sortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase);

            IQueryable<BrandModel> query = _appDbContext.Brands.AsQueryable();

            switch (sortField.ToLower())
            {
                case "brandname":
                    query = ascending ? query.OrderBy(p => p.brandName) : query.OrderByDescending(p => p.brandName);
                    break;
                case "brandcode":
                    query = ascending ? query.OrderBy(p => p.brandCode) : query.OrderByDescending(p => p.brandCode);
                    break;
                default:
                    query = ascending ? query.OrderBy(p => p.brandId) : query.OrderByDescending(p => p.brandId);
                    break;
            }

            List<BrandModel> list = query
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (list, pageCount);
        }

        // GET action to display the list of brands with sorting and pagination
        [ActionName("Index")]
        public IActionResult BrandIndex(int pageNo = 1, int pageSize = 10, string sortField = "", string sortOrder = "asc")
        {
            try
            {
                var (list, pageCount) = GetSorted(pageNo, pageSize, sortField, sortOrder);

                BrandResponseModel response = new()
                {
                    brandData = list,
                    pageSize = pageSize,
                    pageCount = pageCount,
                    pageNo = pageNo,
                    sortField = sortField,
                    sortOrder = sortOrder
                };

                return View("BrandIndex", response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving and sorting brands.");
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return View("BrandIndex");
            }
        }

        // GET action to show the brand creation form
        [ActionName("Create")]
        public IActionResult BrandCreate()
        {
            return View("BrandCreate");
        }

        // POST action to save a new brand
        [HttpPost]
        [ActionName("Save")]
        public IActionResult BrandSave(BrandModel brandModel)
        {
            try
            {
                brandModel.brandCreatedAt = DateTime.Now.ToString();
                _appDbContext.Brands.Add(brandModel);
                int result = _appDbContext.SaveChanges();
                string message = result > 0 ? "Save Success" : "Save Fail";

                MsgResopnseModel rspModel = new MsgResopnseModel()
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };
                return Json(rspModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving a new brand.");
                MsgResopnseModel rspModel = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
                return Json(rspModel);
            }
        }

        // GET action to show the brand editing form
        [ActionName("Edit")]
        public IActionResult BrandEdit(int id)
        {
            try
            {
                var item = _appDbContext.Brands.FirstOrDefault(x => x.brandId == id);
                if (item is null)
                {
                    return Redirect("/Brand");
                }
                return View("BrandEdit", item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while loading BrandEdit view.");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST action to update an existing brand
        [HttpPost]
        [ActionName("Update")]
        public IActionResult BrandUpdate(int id, BrandModel brandModel)
        {
            MsgResopnseModel rspModel = new MsgResopnseModel();
            try
            {
                var item = _appDbContext.Brands.FirstOrDefault(x => x.brandId == id);
                if (item != null)
                {
                    item.brandName = brandModel.brandName;
                    item.brandDescription = brandModel.brandDescription;
                    item.brandUpdatedAt = DateTime.Now.ToString();
                    item.brandUpdateCount ??= 0;
                    item.brandUpdateCount++;

                    int result = _appDbContext.SaveChanges();
                    rspModel = new MsgResopnseModel
                    {
                        IsSuccess = result > 0,
                        responeMessage = result > 0 ? "Update Success" : "Update Fail"
                    };
                }
                else
                {
                    rspModel = new MsgResopnseModel
                    {
                        IsSuccess = false,
                        responeMessage = "Brand not found."
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating the brand.");
                rspModel = new MsgResopnseModel
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
            }

            return Json(rspModel);
        }

        // POST action to delete a brand
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult BrandDelete(int id)
        {
            MsgResopnseModel rspModel = new MsgResopnseModel();
            try
            {
                var item = _appDbContext.Brands.FirstOrDefault(x => x.brandId == id);
                if (item != null)
                {
                    _appDbContext.Brands.Remove(item);
                    int result = _appDbContext.SaveChanges();
                    rspModel = new MsgResopnseModel
                    {
                        IsSuccess = result > 0,
                        responeMessage = result > 0 ? "Delete Success" : "Delete Fail"
                    };
                }
                else
                {
                    rspModel = new MsgResopnseModel
                    {
                        IsSuccess = false,
                        responeMessage = "Brand not found."
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting the brand.");
                rspModel = new MsgResopnseModel
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
            }

            return Json(rspModel);
        }
    }
}
