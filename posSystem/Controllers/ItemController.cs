using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using posSystem.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace posSystem.Controllers
{
    public class ItemController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExcelService _fileUploadService;

        public ItemController(AppDbContext appDbContext, IExcelService fileUploadService)
        {
            _appDbContext = appDbContext;
            _fileUploadService = fileUploadService;
        }

        [HttpGet]
        [ActionName("MassUpload")]
        public IActionResult MassUpload()
        {
            bool hasData = _appDbContext.Items.Any(); // Replace with your actual data check
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
                var items = _fileUploadService.ReadFromExcel<ItemModel>(file);

                foreach (var item in items)
                {
                    var catItem = _appDbContext.Categories.FirstOrDefault(c => c.catCode == item.catCode)!;

                    var brandItem = _appDbContext.Brands.FirstOrDefault(c => c.brandCode == item.brandCode)!;

                    var catSubItem = _appDbContext.SubCategories.FirstOrDefault(c => c.subCatCode == item.subCatCode)!;

                    var subBrandItem = _appDbContext.SubBrands.FirstOrDefault(c => c.subBrandCode == item.subBrandCode)!;


                    item.catId = catItem.catId;
                    item.subCId = catSubItem.subCId;
                    item.subBId = subBrandItem.subBId;
                    item.brandId = brandItem.brandId;
                    item.itemCreateAt = DateTime.Now.ToString();
                    _appDbContext.Items.Add(item);
                }

                int result = _appDbContext.SaveChanges(); // Synchronous save

                rspModel = new MsgResopnseModel
                {
                    IsSuccess = result > 0,
                    responeMessage = result > 0 ? "Items saved successfully!" : "Failed to save items."
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
                var updateItems = _fileUploadService.ReadFromExcel<ItemModel>(file);

                // Fetch all necessary data in one go to minimize database calls
                var itemCodes = updateItems.Select(i => i.itemCode).ToList();
                var existingItems = _appDbContext.Items.Where(c => itemCodes.Contains(c.itemCode)).ToList();

                var catCodes = updateItems.Select(i => i.catCode).ToList();
                var catItems = _appDbContext.Categories.Where(c => catCodes.Contains(c.catCode)).ToDictionary(c => c.catCode, c => c);

                var brandCodes = updateItems.Select(i => i.brandCode).ToList();
                var brandItems = _appDbContext.Brands.Where(c => brandCodes.Contains(c.brandCode)).ToDictionary(c => c.brandCode, c => c);

                var subCatCodes = updateItems.Select(i => i.subCatCode).ToList();
                var subCatItems = _appDbContext.SubCategories.Where(c => subCatCodes.Contains(c.subCatCode)).ToDictionary(c => c.subCatCode, c => c);

                var subBrandCodes = updateItems.Select(i => i.subBrandCode).ToList();
                var subBrandItems = _appDbContext.SubBrands.Where(c => subBrandCodes.Contains(c.subBrandCode)).ToDictionary(c => c.subBrandCode, c => c);

                foreach (var item in updateItems)
                {
                    var existingItem = existingItems.FirstOrDefault(c => c.itemCode == item.itemCode);

                    if (existingItem != null)
                    {
                        if (catItems.TryGetValue(item.catCode, out var catItem))
                        {
                            existingItem.catId = catItem.catId;
                            existingItem.catCode = catItem.catCode;
                        }

                        if (brandItems.TryGetValue(item.brandCode, out var brandItem))
                        {
                            existingItem.brandId = brandItem.brandId;
                            existingItem.brandCode = brandItem.brandCode;
                        }

                        if (subCatItems.TryGetValue(item.subCatCode, out var subCatItem))
                        {
                            existingItem.subCId = subCatItem.subCId;
                            existingItem.subCatCode = subCatItem.subCatCode;
                        }

                        if (subBrandItems.TryGetValue(item.subBrandCode, out var subBrandItem))
                        {
                            existingItem.subBId = subBrandItem.subBId;
                            existingItem.subBrandCode = subBrandItem.subBrandCode;
                        }

                        existingItem.itemCode = item.itemCode;
                        existingItem.itemName = item.itemName;
                        existingItem.itemBuyPrice = item.itemBuyPrice;
                        existingItem.itemSalePrice = item.itemSalePrice;
                        existingItem.itemWholeSalePrice = item.itemWholeSalePrice;
                        existingItem.itemStock = item.itemStock;
                        existingItem.itemSold = item.itemSold;
                        existingItem.itemRemainStock = item.itemRemainStock;
                        existingItem.itemUpdateAt = DateTime.Now.ToString();
                        existingItem.itemUpdateCount = (existingItem.itemUpdateCount ?? 0) + 1;
                    }
                }

                int result = await _appDbContext.SaveChangesAsync();
                string message = result > 0 ? "Items update successfully!" : "Failed to update items.";

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



        private (List<ItemModel>, int) GetSorted(int pageNo, int pageSize, string sortField, string sortOrder)
        {
            try
            {
                int rowCount = _appDbContext.Items.Count();
                int pageCount = rowCount / pageSize;

                if (rowCount % pageSize > 0)
                    pageCount++;

                bool ascending = sortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase);
                IQueryable<ItemModel> query = _appDbContext.Items.AsQueryable();

                switch (sortField.ToLower())
                {
                    case "itemcode":
                        query = ascending ? query.OrderBy(p => p.itemCode) : query.OrderByDescending(p => p.itemCode);
                        break;
                    case "itemname":
                        query = ascending ? query.OrderBy(p => p.itemName) : query.OrderByDescending(p => p.itemName);
                        break;
                    case "itembuyprice":
                        query = ascending ? query.OrderBy(p => p.itemBuyPrice) : query.OrderByDescending(p => p.itemBuyPrice);
                        break;
                    case "itemsaleprice":
                        query = ascending ? query.OrderBy(p => p.itemSalePrice) : query.OrderByDescending(p => p.itemSalePrice);
                        break;
                    case "itemwholesaleprice":
                        query = ascending ? query.OrderBy(p => p.itemWholeSalePrice) : query.OrderByDescending(p => p.itemWholeSalePrice);
                        break;
                    case "itemstock":
                        query = ascending ? query.OrderBy(p => p.itemStock) : query.OrderByDescending(p => p.itemStock);
                        break;
                    case "itemsold":
                        query = ascending ? query.OrderBy(p => p.itemSold) : query.OrderByDescending(p => p.itemSold);
                        break;
                    case "itemremainstock":
                        query = ascending ? query.OrderBy(p => p.itemRemainStock) : query.OrderByDescending(p => p.itemRemainStock);
                        break;
                    case "itemcategory":
                        query = ascending ? query.OrderBy(p => p.itemCategory) : query.OrderByDescending(p => p.itemCategory);
                        break;
                    case "itemsubcategory":
                        query = ascending ? query.OrderBy(p => p.itemSubCategory) : query.OrderByDescending(p => p.itemSubCategory);
                        break;
                    case "itembrand":
                        query = ascending ? query.OrderBy(p => p.itemBrand) : query.OrderByDescending(p => p.itemBrand);
                        break;
                    case "itemsubbrand":
                        query = ascending ? query.OrderBy(p => p.itemSubBrand) : query.OrderByDescending(p => p.itemSubBrand);
                        break;
                    default:
                        query = ascending ? query.OrderBy(p => p.itemId) : query.OrderByDescending(p => p.itemId);
                        break;
                }

                List<ItemModel> list = query
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .Include(s => s.Category)
                    .Include(s => s.SubCategory) // Assuming you have a navigation property for SubCategory
                    .Include(s => s.Brand)
                    .Include(s => s.SubBrand)
                    .Select(s => new ItemModel
                    {
                        itemId = s.itemId,
                        itemCode = s.itemCode,
                        itemName = s.itemName,
                        itemBuyPrice = s.itemBuyPrice,
                        itemSalePrice = s.itemSalePrice,
                        itemWholeSalePrice = s.itemWholeSalePrice,
                        catCode = s.catCode,
                        subCatCode = s.subCatCode,
                        itemDescription = s.itemDescription,
                        itemBarcode = s.itemBarcode,
                        itemStock = s.itemStock,
                        itemSold = s.itemSold,
                        itemRemainStock = s.itemRemainStock,
                        itemCreateAt = s.itemCreateAt,
                        itemUpdateAt = s.itemUpdateAt,
                        itemUpdateCount = s.itemUpdateCount,
                        creatorName = s.creatorName,
                        catId = s.catId,
                        subCId = s.subCId,

                        itemCategory = _appDbContext.Categories
                            .Where(c => c.catCode == s.catCode || c.catId == s.catId)
                            .Select(c => c.catName)
                            .FirstOrDefault(), // Retrieve catName based on catId

                        itemSubCategory = _appDbContext.SubCategories
                            .Where(sc => sc.subCatCode == s.subCatCode || sc.subCId == s.subCId)
                            .Select(sc => sc.subCatName)
                            .FirstOrDefault(), // Retrieve subCatName based on subCId

                        itemBrand = _appDbContext.Brands
                            .Where(c => c.brandCode == s.brandCode || c.brandId == s.brandId)
                            .Select(c => c.brandName)
                            .FirstOrDefault(), // Retrieve brandName based on brandId

                        itemSubBrand = _appDbContext.SubBrands
                            .Where(sc => sc.subBrandCode == s.subBrandCode || sc.subBId == s.subBId)
                            .Select(sc => sc.subBrandName)
                            .FirstOrDefault(), // Retrieve subBrandName based on subBId
                    })
                    .ToList()!;
                return (list, pageCount);
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                Console.WriteLine($"An error occurred while sorting: {ex.Message}");
                throw; // Re-throw the exception to be caught in the calling method
            }
        }

        [ActionName("Index")]
        public IActionResult ItemIndex(int pageNo = 1, int pageSize = 10, string sortField = "", string sortOrder = "asc")
        {
            try
            {
                var (list, pageCount) = GetSorted(pageNo, pageSize, sortField, sortOrder);

                ItemResponseModel response = new()
                {
                    itemData = list,
                    pageSize = pageSize,
                    pageCount = pageCount,
                    pageNo = pageNo,
                    sortField = sortField,
                    sortOrder = sortOrder
                };
                return View("ItemIndex", response);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while loading items: {ex.Message}");
                return View("ItemIndex");
            }
        }

        [ActionName("CheckLowStock")]
        public IActionResult CheckLowStock()
        {
            try
            {
                var lowStockItems = _appDbContext.Items
                    .Where(x => x.itemRemainStock <= 2)
                    .ToList();

                var response = new
                {
                    lowStockCount = lowStockItems.Count,
                    isStockLow = lowStockItems.Count > 0
                };

                return Json(response); // Return the data as JSON
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while checking low stock: {ex.Message}");
                return Json(new { lowStockCount = 0, isStockLow = false });
            }
        }

        [ActionName("LowStock")]
        public IActionResult LowStock(int pageNo = 1, int pageSize = 10)
        {
            try
            {
                // Get the total number of low-stock items
                int rowCount = _appDbContext.Items.Count(x => x.itemRemainStock <= 2);

                // Calculate the total number of pages
                int pageCount = (int)Math.Ceiling((double)rowCount / pageSize);

                // Retrieve the low-stock items with pagination
                var items = _appDbContext.Items
                    .Where(x => x.itemRemainStock <= 2)
                    .Include(x => x.Category)
                    .Include(x => x.SubCategory)
                    .Include(x => x.Brand)
                    .Include(x => x.SubBrand)
                    .OrderBy(x => x.itemId) // Ensure a consistent ordering
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                // Prepare the response model
                ItemResponseModel response = new()
                {
                    itemData = items.Select(s => new ItemModel
                    {
                        itemId = s.itemId,
                        itemCode = s.itemCode,
                        itemName = s.itemName,
                        itemBuyPrice = s.itemBuyPrice,
                        itemSalePrice = s.itemSalePrice,
                        itemWholeSalePrice = s.itemWholeSalePrice,
                        catCode = s.catCode,
                        subCatCode = s.subCatCode,
                        itemDescription = s.itemDescription,
                        itemBarcode = s.itemBarcode,
                        itemStock = s.itemStock,
                        itemSold = s.itemSold,
                        itemRemainStock = s.itemRemainStock,
                        itemCreateAt = s.itemCreateAt,
                        itemUpdateAt = s.itemUpdateAt,
                        itemUpdateCount = s.itemUpdateCount,
                        creatorName = s.creatorName,
                        catId = s.catId,
                        subCId = s.subCId,
                        itemCategory = s.Category?.catName, // Retrieve catName from included Category
                        itemSubCategory = s.SubCategory?.subCatName, // Retrieve subCatName from included SubCategory
                        itemBrand = s.Brand?.brandName, // Retrieve brandName from included Brand
                        itemSubBrand = s.SubBrand?.subBrandName // Retrieve subBrandName from included SubBrand
                    }).ToList(),
                    pageSize = pageSize,
                    pageCount = pageCount,
                    pageNo = pageNo,
                };

                return View("LowStock", response);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while loading low stock items: {ex.Message}");
                return View("LowStock", new ItemResponseModel()); // Return an empty response model in case of an error
            }
        }



        [ActionName("Create")]
        public IActionResult ItemCreate()
        {
            try
            {
                var categories = _appDbContext.Categories.ToList();
                var subCategories = _appDbContext.SubCategories.ToList();
                var brands = _appDbContext.Brands.ToList();
                var subBrands = _appDbContext.SubBrands.ToList();
                ViewBag.Categories = categories;
                ViewBag.SubCategories = subCategories;
                ViewBag.Brands = brands;
                ViewBag.SubBrands = subBrands;

                return View("ItemCreate");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while loading create item page: {ex.Message}");
                return View("ItemCreate");
            }
        }

        [HttpPost]
        [ActionName("Save")]
        public IActionResult ItemSave(ItemModel itemModel)
        {
            try
            {
                var catItem = _appDbContext.Categories.FirstOrDefault(c => c.catId == itemModel.catId);
                if (catItem == null) throw new Exception("Category not found");

                itemModel.catCode = catItem.catCode;

                var subCatItem = _appDbContext.SubCategories.FirstOrDefault(c => c.subCId == itemModel.subCId);
                if (subCatItem == null) throw new Exception("SubCategory not found");

                itemModel.subCatCode = subCatItem.subCatCode;

                var brandItem = _appDbContext.Brands.FirstOrDefault(c => c.brandId == itemModel.brandId);
                if (brandItem == null) throw new Exception("Brand not found");

                itemModel.brandCode = brandItem.brandCode;

                var subBrandItem = _appDbContext.SubBrands.FirstOrDefault(c => c.subBId == itemModel.subBId);
                if (subBrandItem == null) throw new Exception("SubBrand not found");

                itemModel.subBrandCode = subBrandItem.subBrandCode;

                itemModel.itemCreateAt = DateTime.Now.ToString();
                itemModel.itemRemainStock = itemModel.itemStock;
                _appDbContext.Items.Add(itemModel);
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
                return Json(new MsgResopnseModel
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred while saving item: {ex.Message}"
                });
            }
        }

        

        [ActionName("Edit")]
        public IActionResult ItemEdit(int itemId)
        {
            try
            {
                var item = _appDbContext.Items
                    .Include(i => i.Brand)
                    .Include(i => i.SubBrand)
                    .Include(i => i.Category)
                    .Include(i => i.SubCategory)
                    .FirstOrDefault(x => x.itemId == itemId);

                if (item is null)
                {
                    return Redirect("/Item");
                }

                // Set ViewBag properties for the associated entities
                if (item.Brand != null)
                {
                    ViewBag.BrandName = item.Brand.brandName;
                    ViewBag.BrandId = item.Brand.brandId;
                }

                if (item.SubBrand != null)
                {
                    ViewBag.SubBrandName = item.SubBrand.subBrandName;
                    ViewBag.SubBId = item.SubBrand.subBId;
                }

                if (item.Category != null)
                {
                    ViewBag.CategoryName = item.Category.catName;
                    ViewBag.CatId = item.Category.catId;
                }

                if (item.SubCategory != null)
                {
                    ViewBag.SubCatName = item.SubCategory.subCatName;
                    ViewBag.SubCId = item.SubCategory.subCId;
                }

                // Fetch all categories, subcategories, brands, and subbrands in a single query each
                ViewBag.Categories = _appDbContext.Categories.ToList();
                ViewBag.SubCategories = _appDbContext.SubCategories.ToList();
                ViewBag.Brands = _appDbContext.Brands.ToList();
                ViewBag.SubBrands = _appDbContext.SubBrands.ToList();

                return View("ItemEdit", item);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while loading the edit item page: {ex.Message}");
                return View("ItemEdit");
            }
        }


        [HttpPost]
        [ActionName("Update")]
        public IActionResult ItemUpdate(ItemModel itemModel)
        {
            try
            {
                var existingItem = _appDbContext.Items.Find(itemModel.itemId);
                if (existingItem == null)
                {
                    return Json(new MsgResopnseModel
                    {
                        IsSuccess = false,
                        responeMessage = "Item not found"
                    });
                }

                var catItem = _appDbContext.Categories.FirstOrDefault(c => c.catId == itemModel.catId);
                if (catItem == null) throw new Exception("Category not found");

                itemModel.catCode = catItem.catCode;

                var subCatItem = _appDbContext.SubCategories.FirstOrDefault(c => c.subCId == itemModel.subCId);
                if (subCatItem == null) throw new Exception("SubCategory not found");

                itemModel.subCatCode = subCatItem.subCatCode;

                var brandItem = _appDbContext.Brands.FirstOrDefault(c => c.brandId == itemModel.brandId);
                if (brandItem == null) throw new Exception("Brand not found");

                itemModel.brandCode = brandItem.brandCode;

                var subBrandItem = _appDbContext.SubBrands.FirstOrDefault(c => c.subBId == itemModel.subBId);
                if (subBrandItem == null) throw new Exception("SubBrand not found");


                existingItem.itemCode = itemModel.itemCode;
                existingItem.itemName = itemModel.itemName;
                existingItem.itemBuyPrice = itemModel.itemBuyPrice;
                existingItem.itemSalePrice = itemModel.itemSalePrice;
                existingItem.itemWholeSalePrice = itemModel.itemWholeSalePrice;
                existingItem.catId = itemModel.catId;
                existingItem.catCode = catItem.catCode;
                existingItem.subCId = itemModel.subCId;
                existingItem.subCatCode = subCatItem.subCatCode;
                existingItem.brandId = itemModel.brandId;
                existingItem.brandCode = brandItem.brandCode;
                existingItem.subBId = itemModel.subBId;
                existingItem.subBrandCode = subBrandItem.subBrandCode;
                existingItem.itemStock = itemModel.itemStock;
                existingItem.itemSold = itemModel.itemSold;
                existingItem.itemRemainStock = itemModel.itemRemainStock;
                existingItem.itemUpdateAt = DateTime.Now.ToString();
                existingItem.itemUpdateCount = itemModel.itemUpdateCount + 1;

                _appDbContext.Items.Update(existingItem);
                int result = _appDbContext.SaveChanges();
                string message = result > 0 ? "Update Success" : "Update Fail";
                return Json(new MsgResopnseModel
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                });
            }
            catch (Exception ex)
            {
                return Json(new MsgResopnseModel
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred while updating item: {ex.Message}"
                });
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ItemDelete(int itemId)
        {
            try
            {
                var item = _appDbContext.Items.Find(itemId);
                if (item == null)
                {
                    return Json(new MsgResopnseModel
                    {
                        IsSuccess = false,
                        responeMessage = "Item not found"
                    });
                }

                _appDbContext.Items.Remove(item);
                int result = _appDbContext.SaveChanges();
                string message = result > 0 ? "Delete Success" : "Delete Fail";
                return Json(new MsgResopnseModel
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                });
            }
            catch (Exception ex)
            {
                return Json(new MsgResopnseModel
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred while deleting item: {ex.Message}"
                });
            }
        }

        [HttpPost]
        [ActionName("DeleteAll")]
        public IActionResult DeleteAllItems()
        {
            MsgResopnseModel rspModel = new MsgResopnseModel();

            try
            {
                var items = _appDbContext.Items.ToList();
                if (items.Count == 0)
                {
                    rspModel = new MsgResopnseModel()
                    {
                        IsSuccess = false,
                        responeMessage = "No items found to delete."
                    };
                    return Json(rspModel);
                }

                _appDbContext.Items.RemoveRange(items);
                int result = _appDbContext.SaveChanges();
                string message = result > 0 ? "All items deleted successfully." : "Failed to delete items.";
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
