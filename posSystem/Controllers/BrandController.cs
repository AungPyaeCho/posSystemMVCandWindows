using Microsoft.AspNetCore.Mvc;
using posSystem.Models;

namespace posSystem.Controllers
{
    public class BrandController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExcelService _fileUploadService;

        // Constructor to set up the controller with database context and file upload service
        public BrandController(AppDbContext appDbContext, IExcelService fileUploadService)
        {
            _appDbContext = appDbContext;
            _fileUploadService = fileUploadService;
        }

        // GET action to show the mass upload form
        [HttpGet]
        [ActionName("MassUpload")]
        public IActionResult MassUpload()
        {
            // Check if there are any categories in the database
            bool hasData = _appDbContext.Brands.Any();
            ViewBag.HasData = hasData;
            return View();
        }

        // POST action to handle the mass upload of brands from an Excel file
        [HttpPost]
        [ActionName("MassUpload")]
        public IActionResult MassUpload(IFormFile file)
        {
            var rspModel = new MsgResopnseModel();

            // Check if the file is missing or empty
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
                // Read brands from the uploaded Excel file
                var brands = _fileUploadService.ReadFromExcel<BrandModel>(file);

                // Add each brand to the database
                foreach (var item in brands)
                {
                    item.brandCreatedAt = DateTime.Now.ToString();
                    _appDbContext.Brands.Add(item);
                }

                int result = _appDbContext.SaveChanges(); // Save changes to the database

                rspModel = new MsgResopnseModel
                {
                    IsSuccess = result > 0,
                    responeMessage = result > 0 ? "Brands saved successfully!" : "Failed to save brands."
                };
            }
            catch (Exception ex)
            {
                // Handle errors during file processing or database operations
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

            // Check if the file is missing or empty
            if (file == null || file.Length == 0)
            {
                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = "No file uploaded. Please select an Excel file to update brands."
                };
                return Json(rspModel);
            }

            // Check if the file is an Excel file
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
                // Read updated brands from the Excel file
                var updatedBrands = _fileUploadService.ReadFromExcel<BrandModel>(file);

                // Update existing brands in the database
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

                int result = await _appDbContext.SaveChangesAsync(); // Save changes to the database asynchronously
                string message = result > 0 ? "Brands updated successfully!" : "Failed to update brands.";

                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };
            }
            catch (Exception ex)
            {
                // Handle errors during file processing or database operations
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
            int rowCount = _appDbContext.Brands.Count(); // Get total count of brands
            int pageCount = rowCount / pageSize;

            if (rowCount % pageSize > 0)
                pageCount++;

            bool ascending = sortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase);

            IQueryable<BrandModel> query = _appDbContext.Brands.AsQueryable();

            // Sort the brands based on the sortField and sortOrder
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

            // Get the brands for the current page
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
                // Handle errors during data retrieval and sorting
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
                int result = _appDbContext.SaveChanges(); // Save changes to the database
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
                // Handle errors during the save operation
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
            var item = _appDbContext.Brands.FirstOrDefault(x => x.brandId == id);
            if (item is null)
            {
                return Redirect("/Brand");
            }
            return View("BrandEdit", item);
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
                if (item is null)
                {
                    rspModel = new MsgResopnseModel()
                    {
                        IsSuccess = false,
                        responeMessage = "No data found"
                    };
                    return Json(rspModel);
                }

                // Update brand details
                item.brandName = brandModel.brandName;
                item.brandCode = brandModel.brandCode;
                item.brandDescription = brandModel.brandDescription;
                item.brandUpdatedAt = DateTime.Now.ToString();
                item.brandUpdateCount ??= 0;
                item.brandUpdateCount++;

                int result = _appDbContext.SaveChanges(); // Save changes to the database
                string message = result > 0 ? "Update Success" : "Update Fail";

                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };
            }
            catch (Exception ex)
            {
                // Handle errors during the update operation
                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
            }
            return Json(rspModel);
        }

        // POST action to delete a specific brand
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult BrandDelete(int id)
        {
            MsgResopnseModel rspModel = new MsgResopnseModel();
            try
            {
                var item = _appDbContext.Brands.FirstOrDefault(x => x.brandId == id);
                if (item is null)
                {
                    rspModel = new MsgResopnseModel()
                    {
                        IsSuccess = false,
                        responeMessage = "No data found"
                    };
                    return Json(rspModel);
                }

                _appDbContext.Brands.Remove(item);
                int result = _appDbContext.SaveChanges(); // Save changes to the database
                string message = result > 0 ? "Delete Success" : "Delete Fail";

                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };
            }
            catch (Exception ex)
            {
                // Handle errors during the delete operation
                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
            }
            return Json(rspModel);
        }

        // POST action to delete all brands
        [HttpPost]
        [ActionName("DeleteAll")]
        public IActionResult DeleteAllCategories()
        {
            MsgResopnseModel rspModel = new MsgResopnseModel();

            try
            {
                var items = _appDbContext.Brands.ToList();
                if (items.Count == 0)
                {
                    rspModel = new MsgResopnseModel()
                    {
                        IsSuccess = false,
                        responeMessage = "No brands found to delete."
                    };
                    return Json(rspModel);
                }

                _appDbContext.Brands.RemoveRange(items);
                int result = _appDbContext.SaveChanges(); // Save changes to the database
                string message = result > 0 ? "All brands deleted successfully." : "Failed to delete brands.";

                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };
            }
            catch (Exception ex)
            {
                // Handle errors during the delete all operation
                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = "You have to delete sub-brands first!"
                };
            }

            return Json(rspModel);
        }
    }
}
