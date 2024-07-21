using Microsoft.AspNetCore.Mvc;
using posSystem.Models;
using System.Drawing.Printing;


namespace posSystem.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExcelService _fileUploadService;

        public CategoryController(AppDbContext appDbContext, IExcelService fileUploadService)
        {
            _appDbContext = appDbContext;
            _fileUploadService = fileUploadService;
        }

        [HttpGet]
        [ActionName("MassUpload")]
        public IActionResult MassUpload()
        {
            bool hasData = _appDbContext.Categories.Any(); // Replace with your actual data check
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

                int result = _appDbContext.SaveChanges(); // Synchronous save

                rspModel = new MsgResopnseModel
                {
                    IsSuccess = result > 0,
                    responeMessage = result > 0 ? "Categories saved successfully!" : "Failed to save categories."
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
                var updatedCategories = _fileUploadService.ReadFromExcel<CategoryModel>(file);

                // Iterate over each category from the file
                foreach (var updatedCategory in updatedCategories)
                {
                    // Find existing category by catCode
                    var existingCategory = _appDbContext.Categories
                        .FirstOrDefault(c => c.catCode == updatedCategory.catCode);

                    if (existingCategory != null)
                    {
                        // Update properties
                        existingCategory.catName = updatedCategory.catName;
                        existingCategory.catDescription = updatedCategory.catDescription;
                        existingCategory.catUpdatedAt = DateTime.Now.ToString();
                        existingCategory.catUpdateCount ??= 0;
                        existingCategory.catUpdateCount++;
                    }
                }

                // Save changes to the database
                int result = await _appDbContext.SaveChangesAsync();
                string message = result > 0 ? "Categories update successfully!" : "Failed to update categories.";

                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };
            }
            catch (Exception ex)
            {
                // Log the exception (if logging is set up)
                // _logger.LogError(ex, "Error occurred while updating categories.");

                rspModel = new MsgResopnseModel()
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

            categoryModel.catCreatedAt = DateTime.Now.ToString();
            _appDbContext.Categories.Add(categoryModel);
            int result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Save Success" : "Save Fail";
            MsgResopnseModel rspModel = new MsgResopnseModel()
            {
                IsSuccess = result > 0,
                responeMessage = message
            };
            return Json(rspModel);
        }

        [ActionName("Edit")]
        public IActionResult CategoryEdit(int id)
        {
            var item = _appDbContext.Categories.FirstOrDefault(x => x.catId == id);
            if (item is null)
            {
                return Redirect("/Category");
            }
            return View("CategoryEdit", item);
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult CategoryUpdate(int catId, CategoryModel categoryModel)
        {
            Console.WriteLine($"Received catId: {catId}");
            MsgResopnseModel rspModel = new MsgResopnseModel();
            var item = _appDbContext.Categories.FirstOrDefault(x => x.catId == catId);
            if (item is null)
            {
                rspModel = new MsgResopnseModel()
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
            rspModel = new MsgResopnseModel()
            {
                IsSuccess = result > 0,
                responeMessage = message
            };
            return Json(rspModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult CategoryDelete(int catId)
        {
            MsgResopnseModel rspModel = new MsgResopnseModel();
            var item = _appDbContext.Categories.FirstOrDefault(x => x.catId == catId);
            if (item is null)
            {
                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = "No data found"
                };
                return Json(rspModel);
            }

            _appDbContext.Categories.Remove(item);
            int result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Delete Success" : "Delete Fail";
            rspModel = new MsgResopnseModel()
            {
                IsSuccess = result > 0,
                responeMessage = message
            };
            return Json(rspModel);
        }

        [HttpPost]
        [ActionName("DeleteAll")]
        public IActionResult DeleteAllCategories()
        {
            MsgResopnseModel rspModel = new MsgResopnseModel();

            var items = _appDbContext.Categories.ToList();
            if (items.Count is 0)
            {
                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = "No categories found to delete."
                };
                return Json(rspModel);
            }

            _appDbContext.Categories.RemoveRange(items);
            int result = _appDbContext.SaveChanges();
            string message = result > 0 ? "All categories deleted successfully." : "Failed to delete categories.";
            rspModel = new MsgResopnseModel()
            {
                IsSuccess = result > 0,
                responeMessage = message
            };
            return Json(rspModel);
        }
    }
}
