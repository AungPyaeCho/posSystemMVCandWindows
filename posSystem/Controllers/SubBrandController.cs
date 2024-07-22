using Microsoft.AspNetCore.Mvc;
using posSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace posSystem.Controllers
{
    public class SubBrandController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExcelService _fileUploadService;

        public SubBrandController(AppDbContext appDbContext, IExcelService fileUploadService)
        {
            _appDbContext = appDbContext;
            _fileUploadService = fileUploadService;
        }

        [HttpGet]
        [ActionName("MassUpload")]
        public IActionResult MassUpload()
        {
            bool hasData = _appDbContext.SubBrands.Any(); // Replace with your actual data check
            ViewBag.HasData = hasData;
            return View();
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
                    var brandItem = _appDbContext.Brands.FirstOrDefault(c => c.brandCode == items.brandCode)!;
                    items.brandId = brandItem.brandId;
                    items.subBrandCreateAt = DateTime.Now.ToString();
                    _appDbContext.SubBrands.Add(items);
                }

                int result = _appDbContext.SaveChanges(); // Synchronous save

                rspModel = new MsgResopnseModel
                {
                    IsSuccess = result > 0,
                    responeMessage = result > 0 ? "Sub-Brands saved successfully!" : "Failed to save sub-brands."
                };
            }
            catch (Exception ex)
            {
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
                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = "No file uploaded. Please select an Excel file to update categories."
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
                // Read categories from the Excel file
                var updateSubBrands = _fileUploadService.ReadFromExcel<SubBrandModel>(file);

                // Iterate over each category from the file
                foreach (var items in updateSubBrands)
                {
                    // Find existing category by catCode
                    var existingBrands = _appDbContext.SubBrands
                        .FirstOrDefault(c => c.subBrandCode == items.subBrandCode);

                    if (existingBrands is not null)
                    {
                        var brandItem = _appDbContext.SubBrands.FirstOrDefault(c => c.brandCode == items.brandCode)!;

                        // Update properties
                        existingBrands.subBrandName = items.subBrandName;


                        existingBrands.brandId = brandItem.brandId;


                        existingBrands.subBrandCode = items.subBrandCode;

                        existingBrands.brandCode = items.brandCode;
                        existingBrands.subBrandUpdateAt = DateTime.Now.ToString();
                        existingBrands.subBrandUpdateCount ??= 0;
                        existingBrands.subBrandUpdateCount++;
                    }
                }

                // Save changes to the database
                int result = await _appDbContext.SaveChangesAsync();
                string message = result > 0 ? "Sub-Brands update successfully!" : "Failed to update sub-brands.";

                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };
            }
            catch (Exception ex)
            {
                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
            }

            return Json(rspModel);
        }

        private (List<SubBrandModel>, int) GetSorted(int pageNo, int pageSize, string sortField, string sortOrder)
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
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return View("SubBrandIndex");
            }
        }

        [ActionName("Create")]
        public IActionResult SubBrandCreate()
        {
            var brands = _appDbContext.Brands.ToList();
            ViewBag.Brands = brands;
            return View("SubBrandCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public IActionResult SubBrandSave(SubBrandModel subBrandModel)
        {
            var brandItem = _appDbContext.Brands.FirstOrDefault(c => c.brandId == subBrandModel.brandId)!;
            subBrandModel.brandCode = brandItem.brandCode;
            subBrandModel.subBrandCreateAt = DateTime.Now.ToString();
            _appDbContext.SubBrands.Add(subBrandModel);
            int result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Save Success" : "Save Fail";
            MsgResopnseModel rspModel = new MsgResopnseModel()
            {
                IsSuccess = result > 0,
                responeMessage = message
            }!;
            return Json(rspModel);
        }

        [ActionName("Edit")]
        public IActionResult SubBrandEdit(int subBId)
        {
            var item = _appDbContext.SubBrands.FirstOrDefault(x => x.subBId == subBId);
            if (item is null)
            {
                return Redirect("/SubBrand");
            }

            // Get the corresponding category for the subcategory
            var brand = _appDbContext.Brands
                .FirstOrDefault(c => c.brandId == item.brandId && c.brandCode == item.brandCode);

            // If the category is found, add its name to the ViewBag
            if (brand != null)
            {
                ViewBag.BrandName = brand.brandName;
                ViewBag.BrandId = brand.brandId;
            }

            var brands = _appDbContext.Brands.ToList();
            ViewBag.Brands = brands;

            return View("SubBrandEdit", item);
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult SubBrandUpdate(int subBId, SubBrandModel subBrandModel)
        {
            MsgResopnseModel rspModel = new MsgResopnseModel()!;
            var item = _appDbContext.SubBrands.FirstOrDefault(x => x.subBId == subBId);
            if (item is null)
            {
                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = "No data found"
                };
                return Json(rspModel);
            }

            var brandItem = _appDbContext.Brands.FirstOrDefault(c => c.brandId == subBrandModel.brandId)!;
            item.subBrandName = subBrandModel.subBrandName;
            item.subBrandCode = subBrandModel.subBrandCode;
            item.brandId = subBrandModel.brandId;
            item.brandCode = brandItem.brandCode;
            item.subBrandUpdateAt = DateTime.Now.ToString();
            item.subBrandUpdateCount ??= 0;
            item.subBrandUpdateCount++;

            int result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Update Success" : "Update Fail";
            rspModel = new MsgResopnseModel()
            {
                IsSuccess = result > 0,
                responeMessage = message
            };
            return Json(rspModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult SubBrandDelete(int subBId)
        {
            MsgResopnseModel rspModel = new MsgResopnseModel();
            try
            {
                var item = _appDbContext.SubBrands.FirstOrDefault(x => x.subBId == subBId);
                if (item is null)
                {
                    rspModel = new MsgResopnseModel()
                    {
                        IsSuccess = false,
                        responeMessage = "No data found"
                    };
                    return Json(rspModel);
                }

                _appDbContext.SubBrands.Remove(item);
                int result = _appDbContext.SaveChanges();
                string message = result > 0 ? "Delete Success" : "Delete Fail";
                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };
            }
            catch (Exception ex)
            {
                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
            }
            return Json(rspModel);
        }

        [HttpPost]
        [ActionName("DeleteAll")]
        public IActionResult DeleteAllSubBrands()
        {
            MsgResopnseModel rspModel = new MsgResopnseModel();
            try
            {
                var items = _appDbContext.SubBrands.ToList();
                if (items.Count == 0)
                {
                    rspModel = new MsgResopnseModel()
                    {
                        IsSuccess = false,
                        responeMessage = "No sub-brands found to delete."
                    };
                    return Json(rspModel);
                }

                _appDbContext.SubBrands.RemoveRange(items);
                int result = _appDbContext.SaveChanges();
                string message = result > 0 ? "All sub-brands deleted successfully." : "Failed to delete sub-brands.";
                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };
            }
            catch (Exception ex)
            {
                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
            }
            return Json(rspModel);
        }

    }
}
