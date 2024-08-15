using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using posSystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace posSystem.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExcelService _fileUploadService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(AppDbContext appDbContext, IExcelService fileUploadService, ILogger<CategoryController> logger)
        {
            _appDbContext = appDbContext;
            _fileUploadService = fileUploadService;
            _logger = logger;
        }

        [HttpGet]
        [ActionName("MassUpload")]
        public IActionResult MassUpload()
        {
            bool hasData = _appDbContext.Categories.Any();
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
                var categories = _fileUploadService.ReadFromExcel<CategoryModel>(file);

                foreach (var category in categories)
                {
                    category.catCreatedAt = DateTime.Now.ToString();
                    _appDbContext.Categories.Add(category);
                }

                int result = _appDbContext.SaveChanges();

                rspModel = new MsgResopnseModel
                {
                    IsSuccess = result > 0,
                    responeMessage = result > 0 ? "Categories saved successfully!" : "Failed to save categories."
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

        [HttpPost]
        [ActionName("MassUpdate")]
        public async Task<IActionResult> MassUpdate(IFormFile file)
        {
            var rspModel = new MsgResopnseModel();

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
                var updatedCategories = _fileUploadService.ReadFromExcel<CategoryModel>(file);

                foreach (var updatedCategory in updatedCategories)
                {
                    var existingCategory = _appDbContext.Categories
                        .FirstOrDefault(c => c.catCode == updatedCategory.catCode);

                    if (existingCategory != null)
                    {
                        existingCategory.catName = updatedCategory.catName;
                        existingCategory.catDescription = updatedCategory.catDescription;
                        existingCategory.catUpdatedAt = DateTime.Now.ToString();
                        existingCategory.catUpdateCount ??= 0;
                        existingCategory.catUpdateCount++;
                    }
                }

                int result = await _appDbContext.SaveChangesAsync();
                string message = result > 0 ? "Categories updated successfully!" : "Failed to update categories.";

                rspModel = new MsgResopnseModel
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing mass update.");
                rspModel = new MsgResopnseModel
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
            }

            return Json(rspModel);
        }

        private (List<CategoryModel>, int) GetSorted(int pageNo, int pageSize, string sortField, string sortOrder)
        {
            int rowCount = _appDbContext.Categories.Count();
            int pageCount = rowCount / pageSize;

            if (rowCount % pageSize > 0)
                pageCount++;

            bool ascending = sortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase);

            IQueryable<CategoryModel> query = _appDbContext.Categories.AsQueryable();

            switch (sortField.ToLower())
            {
                case "catname":
                    query = ascending ? query.OrderBy(p => p.catName) : query.OrderByDescending(p => p.catName);
                    break;
                case "catcode":
                    query = ascending ? query.OrderBy(p => p.catCode) : query.OrderByDescending(p => p.catCode);
                    break;
                default:
                    query = ascending ? query.OrderBy(p => p.catId) : query.OrderByDescending(p => p.catId);
                    break;
            }

            List<CategoryModel> list = query
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (list, pageCount);
        }

        [ActionName("Index")]
        public IActionResult CategoryIndex(int pageNo = 1, int pageSize = 10, string sortField = "", string sortOrder = "asc")
        {
            try
            {
                var (list, pageCount) = GetSorted(pageNo, pageSize, sortField, sortOrder);

                CategoryResponseModel response = new()
                {
                    categoryData = list,
                    pageSize = pageSize,
                    pageCount = pageCount,
                    pageNo = pageNo,
                    sortField = sortField,
                    sortOrder = sortOrder
                };

                return View("CategoryIndex", response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching category index.");
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return View("CategoryIndex");
            }
        }

        [ActionName("Create")]
        public IActionResult CategoryCreate()
        {
            return View("CategoryCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public IActionResult CategorySave(CategoryModel categoryModel)
        {
            MsgResopnseModel rspModel;

            try
            {
                categoryModel.catCreatedAt = DateTime.Now.ToString();
                _appDbContext.Categories.Add(categoryModel);
                int result = _appDbContext.SaveChanges();
                string message = result > 0 ? "Save Success" : "Save Fail";
                rspModel = new MsgResopnseModel
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving the category.");
                rspModel = new MsgResopnseModel
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
            }

            return Json(rspModel);
        }

        [ActionName("Edit")]
        public IActionResult CategoryEdit(int id)
        {
            var item = _appDbContext.Categories.FirstOrDefault(x => x.catId == id);
            if (item == null)
            {
                _logger.LogWarning("Category with ID {CategoryId} not found for editing.", id);
                return Redirect("/Category");
            }
            return View("CategoryEdit", item);
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult CategoryUpdate(int catId, CategoryModel categoryModel)
        {
            MsgResopnseModel rspModel;

            try
            {
                var item = _appDbContext.Categories.FirstOrDefault(x => x.catId == catId);
                if (item == null)
                {
                    rspModel = new MsgResopnseModel
                    {
                        IsSuccess = false,
                        responeMessage = "No data found"
                    };
                    return Json(rspModel);
                }

                item.catName = categoryModel.catName;
                item.catCode = categoryModel.catCode;
                item.catDescription = categoryModel.catDescription;
                item.catUpdatedAt = DateTime.Now.ToString();
                item.catUpdateCount ??= 0;
                item.catUpdateCount++;

                int result = _appDbContext.SaveChanges();
                string message = result > 0 ? "Update Success" : "Update Fail";
                rspModel = new MsgResopnseModel
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating the category.");
                rspModel = new MsgResopnseModel
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
            }

            return Json(rspModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult CategoryDelete(int catId)
        {
            MsgResopnseModel rspModel;

            try
            {
                var item = _appDbContext.Categories.FirstOrDefault(x => x.catId == catId);
                if (item == null)
                {
                    rspModel = new MsgResopnseModel
                    {
                        IsSuccess = false,
                        responeMessage = "No data found"
                    };
                    return Json(rspModel);
                }

                _appDbContext.Categories.Remove(item);
                int result = _appDbContext.SaveChanges();
                string message = result > 0 ? "Delete Success" : "Delete Fail";
                rspModel = new MsgResopnseModel
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting the category.");
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
