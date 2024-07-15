using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using posSystem.Models;

namespace posSystem.Controllers
{
    public class ItemController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public ItemController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        private (List<ItemModel>, int) GetSorted(int pageNo, int pageSize, string sortField, string sortOrder)
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
                case "itemsaleprice":
                    query = ascending ? query.OrderBy(p => p.itemSalePrice) : query.OrderByDescending(p => p.itemSalePrice);
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
                    query = ascending ? query.OrderBy(p => p.itemSubBrnad) : query.OrderByDescending(p => p.itemSubBrnad);
                    break;
                default:
                    query = ascending ? query.OrderBy(p => p.itemCode) : query.OrderByDescending(p => p.itemCode);
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
                    itemSalePrice = s.itemSalePrice,
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

                    itemSubBrnad = _appDbContext.SubBrands
                        .Where(sc => sc.subBrandCode == s.subBrandCode || sc.subBId == s.subBId)
                        .Select(sc => sc.subBrandName)
                        .FirstOrDefault(), // Retrieve subBrandName based on subBId
                })
                .ToList()!;
            return (list, pageCount);
        }

        [ActionName("Index")]
        public IActionResult ItemIndex(int pageNo = 1, int pageSize = 10, string sortField = "itemName", string sortOrder = "asc")
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
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return View("ItemIndex");
            }
        }


        [ActionName("Create")]
        public IActionResult ItemCreate()
        {
            var categories = _appDbContext.Categories.ToList();
            var subCategories = _appDbContext.SubCategories.ToList();
            ViewBag.Categories = categories;
            ViewBag.SubCategories = subCategories;
            return View("ItemCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public IActionResult ItemSave(ItemModel itemModel)
        {
            var catItem = _appDbContext.Categories.FirstOrDefault(c => c.catId == itemModel.catId)!;
            itemModel.catCode = catItem.catCode;
            var subCatItem = _appDbContext.SubCategories.FirstOrDefault(c => c.subCId == itemModel.subCId)!;
            itemModel.subCatCode = subCatItem.subCatCode;

            itemModel.itemCreateAt = DateTime.Now.ToString();
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

        [ActionName("Edit")]
        public IActionResult ItemEdit(int itemId)
        {
            Console.WriteLine($"Received Id: {itemId}");
            var item = _appDbContext.Items.FirstOrDefault(x => x.itemId == itemId);
            Console.WriteLine($"Received catId: {itemId}");
            if (item is null)
            {
                return Redirect("/Item");
            }

            var categories = _appDbContext.Categories.ToList();
            var subCategories = _appDbContext.SubCategories.ToList();
            ViewBag.Categories = categories;
            ViewBag.SubCategories = subCategories;

            return View("ItemEdit", item);
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult ItemUpdate(int itemId, ItemModel itemModel)
        {
            Console.WriteLine($"Received Id: {itemId}");
            MsgResopnseModel rspModel = new MsgResopnseModel();
            var item = _appDbContext.Items.FirstOrDefault(x => x.itemId == itemId);
            if (item is null)
            {
                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = "No data found"
                };
                return Json(rspModel);
            }

            var catItem = _appDbContext.Categories.FirstOrDefault(c => c.catId == itemModel.catId)!;
            var subCatItem = _appDbContext.SubCategories.FirstOrDefault(s => s.subCId == itemModel.subCId)!;
            var brandItem = _appDbContext.Brands.FirstOrDefault(c => c.brandId == itemModel.brandId)!;
            var subBrandItem = _appDbContext.SubBrands.FirstOrDefault(s => s.subBId == itemModel.subBId)!;

            item.itemCode = itemModel.itemCode;
            item.itemName = itemModel.itemName;
            item.itemBuyPrice = itemModel.itemBuyPrice;
            item.itemSalePrice = itemModel.itemSalePrice;
            item.itemWholeSalePrice = itemModel.itemWholeSalePrice;
            item.catId = itemModel.catId;
            item.catCode = catItem.catCode;
            item.subCId = itemModel.subCId;
            item.subCatCode = subCatItem.subCatCode;
            item.brandId = itemModel.brandId;
            item.brandCode = brandItem.brandCode;
            item.subBId = itemModel.subBId;
            item.subBrandCode = subBrandItem.subBrandCode;
            item.itemColor = itemModel.itemColor;
            item.itemStock = itemModel.itemStock;
            item.itemDescription = itemModel.itemDescription;
            item.itemBarcode = itemModel.itemBarcode;
            item.creatorName = itemModel.creatorName;
            item.itemUpdateAt = DateTime.Now.ToString();
            if (item.itemUpdateCount is null)
            {
                item.itemUpdateCount = 1;
            }
            else
            {
                item.itemUpdateCount++;
            }

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
        public IActionResult SubCategoryDelete(int itemId)
        {
            MsgResopnseModel rspModel = new MsgResopnseModel();
            var item = _appDbContext.Items.FirstOrDefault(x => x.itemId == itemId);
            if (item is null)
            {
                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = "No data found"
                };
                return Json(rspModel);
            }

            _appDbContext.Items.Remove(item);
            int result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Delete Success" : "Delete Fail";
            rspModel = new MsgResopnseModel()
            {
                IsSuccess = result > 0,
                responeMessage = message
            };
            return Json(rspModel);
        }
    }
}
