using Microsoft.AspNetCore.Mvc;
using posSystem.Models;
using System.Text;
using System.Security.Cryptography;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace posSystem.Controllers
{
    public class SubCategoryController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExcelService _fileUploadService;
        private readonly ILogger<SubCategoryController> _logger;

        public SubCategoryController(AppDbContext appDbContext, IExcelService fileUploadService, ILogger<SubCategoryController> logger)
        {
            _appDbContext = appDbContext;
            _fileUploadService = fileUploadService;
            _logger = logger;
        }

        [HttpGet]
        [ActionName("MassUpload")]
        public IActionResult MassUpload()
        {
            bool hasData = _appDbContext.SubCategories.Any(); // Replace with your actual data check
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
                var subCategories = _fileUploadService.ReadFromExcel<SubCategoryModel>(file);

                foreach (var items in subCategories)
                {
                    var catItem = _appDbContext.Categories.FirstOrDefault(c => c.catCode == items.catCode)!;
                    items.catId = catItem.catId;
                    items.subCatCreateAt = DateTime.Now.ToString();
                    _appDbContext.SubCategories.Add(items);
                }

                int result = _appDbContext.SaveChanges(); // Synchronous save

                rspModel = new MsgResopnseModel
                {
                    IsSuccess = result > 0,
                    responeMessage = result > 0 ? "Sub-Categories saved successfully!" : "Failed to save sub-categories."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while uploading and saving sub-categories.");
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
                var updatedSubCategories = _fileUploadService.ReadFromExcel<SubCategoryModel>(file);

                // Iterate over each category from the file
                foreach (var items in updatedSubCategories)
                {
                    // Find existing category by catCode
                    var existingCategory = _appDbContext.SubCategories
                        .FirstOrDefault(c => c.subCatCode == items.subCatCode);

                    if (existingCategory != null)
                    {
                        var catItem = _appDbContext.Categories.FirstOrDefault(c => c.catCode == items.catCode)!;

                        // Update properties
                        existingCategory.subCatName = items.subCatName;
                        existingCategory.catId = catItem.catId;
                        existingCategory.subCatCode = items.subCatCode;
                        existingCategory.catCode = items.catCode;
                        existingCategory.subCatUpdateAt = DateTime.Now.ToString();
                        existingCategory.subCatUpdateCount ??= 0;
                        existingCategory.subCatUpdateCount++;
                    }
                }

                // Save changes to the database
                int result = await _appDbContext.SaveChangesAsync();
                string message = result > 0 ? "Sub-Categories updated successfully!" : "Failed to update sub-categories.";

                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating sub-categories.");
                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
            }

            return Json(rspModel);
        }

        private (List<SubCategoryModel>, int) GetSorted(int pageNo, int pageSize, string sortField, string sortOrder)
        {
            int rowCount = _appDbContext.SubCategories.Count();
            int pageCount = rowCount / pageSize;

            if (rowCount % pageSize > 0)
                pageCount++;

            bool ascending = sortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase);

            IQueryable<SubCategoryModel> query = _appDbContext.SubCategories.AsQueryable();

            switch (sortField.ToLower())
            {
                case "subcatname":
                    query = ascending ? query.OrderBy(p => p.subCatName) : query.OrderByDescending(p => p.subCatName);
                    break;
                case "subcatcode":
                    query = ascending ? query.OrderBy(p => p.subCatCode) : query.OrderByDescending(p => p.subCatCode);
                    break;
                default:
                    query = ascending ? query.OrderBy(p => p.subCId) : query.OrderByDescending(p => p.subCId);
                    break;
            }

            List<SubCategoryModel> list = query
                .Include(s => s.Category)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new SubCategoryModel
                {
                    subCId = s.subCId,
                    subCatName = s.subCatName,
                    subCatCode = s.subCatCode,
                    subCatCreateAt = s.subCatCreateAt,
                    subCatUpdateAt = s.subCatUpdateAt,
                    subCatUpdateCount = s.subCatUpdateCount,
                    catName = _appDbContext.Categories
                        .Where(c => c.catCode == s.catCode || c.catId == s.catId)
                        .Select(c => c.catName)
                        .FirstOrDefault(), // Retrieve catName based on catCod
                })
                .ToList();

            return (list, pageCount);
        }

        [ActionName("Index")]
        public IActionResult SubCategoryIndex(int pageNo = 1, int pageSize = 10, string sortField = "", string sortOrder = "asc")
        {
            try
            {
                var (list, pageCount) = GetSorted(pageNo, pageSize, sortField, sortOrder);

                SubCategoryResponseModel response = new()
                {
                    subCategoryData = list,
                    pageSize = pageSize,
                    pageCount = pageCount,
                    pageNo = pageNo,
                    sortField = sortField,
                    sortOrder = sortOrder
                };

                return View("SubCategoryIndex", response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching sub-categories for the index view.");
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return View("SubCategoryIndex");
            }
        }

        [ActionName("Create")]
        public IActionResult SubCategoryCreate()
        {
            try
            {
                var categories = _appDbContext.Categories.ToList();
                ViewBag.Categories = categories;
                return View("SubCategoryCreate");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while accessing the SubCategoryCreate view.");
                return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier, ErrorMessage = "An error occurred while accessing the SubCategory Create view." });
            }
        }

        [HttpPost]
        [ActionName("Save")]
        public IActionResult SubCategorySave(SubCategoryModel subCategoryModel)
        {
            try
            {
                var catItem = _appDbContext.Categories.FirstOrDefault(c => c.catId == subCategoryModel.catId)!;
                subCategoryModel.catCode = catItem.catCode;
                subCategoryModel.subCatCreateAt = DateTime.Now.ToString();
                _appDbContext.SubCategories.Add(subCategoryModel);
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
                _logger.LogError(ex, "An error occurred while saving sub-category.");
                return Json(new MsgResopnseModel
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                });
            }
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult SubCategoryUpdate(SubCategoryModel subCategoryModel)
        {
            try
            {
                var existingCategory = _appDbContext.SubCategories
                    .FirstOrDefault(c => c.subCId == subCategoryModel.subCId);

                if (existingCategory != null)
                {
                    existingCategory.subCatName = subCategoryModel.subCatName;
                    existingCategory.subCatCode = subCategoryModel.subCatCode;
                    existingCategory.catId = subCategoryModel.catId;
                    existingCategory.subCatUpdateAt = DateTime.Now.ToString();
                    _appDbContext.SubCategories.Update(existingCategory);
                    int result = _appDbContext.SaveChanges();
                    string message = result > 0 ? "Update Success" : "Update Fail";
                    MsgResopnseModel rspModel = new MsgResopnseModel()
                    {
                        IsSuccess = result > 0,
                        responeMessage = message
                    };
                    return Json(rspModel);
                }
                else
                {
                    return Json(new MsgResopnseModel
                    {
                        IsSuccess = false,
                        responeMessage = "SubCategory not found."
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating sub-category.");
                return Json(new MsgResopnseModel
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                });
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult SubCategoryDelete(int id)
        {
            try
            {
                var subCategory = _appDbContext.SubCategories.FirstOrDefault(s => s.subCId == id);

                if (subCategory != null)
                {
                    _appDbContext.SubCategories.Remove(subCategory);
                    int result = _appDbContext.SaveChanges();
                    string message = result > 0 ? "Delete Success" : "Delete Fail";
                    MsgResopnseModel rspModel = new MsgResopnseModel()
                    {
                        IsSuccess = result > 0,
                        responeMessage = message
                    };
                    return Json(rspModel);
                }
                else
                {
                    return Json(new MsgResopnseModel
                    {
                        IsSuccess = false,
                        responeMessage = "SubCategory not found."
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting sub-category.");
                return Json(new MsgResopnseModel
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                });
            }
        }
    }
}
