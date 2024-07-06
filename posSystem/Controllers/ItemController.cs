﻿using Microsoft.AspNetCore.Mvc;
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

        [ActionName("Index")]
        public IActionResult ItemIndex(int pageNo = 1, int pageSize = 10)
        {
            int rowCount = _appDbContext.Items.Count();
            int pageCount = rowCount / pageSize;

            if (rowCount % pageSize > 0)
                pageCount++;

            List<ItemModel> list = _appDbContext.Items
                .Include(s => s.Category)
                .Include(s => s.SubCategory) // Assuming you have a navigation property for SubCategory
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new ItemModel
                {
                    itemId = s.itemId,
                    itemCode = s.itemCode,
                    itemName = s.itemName,
                    itemPrice = s.itemPrice,
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
                                .Where(c => c.catId == s.catId)
                                .Select(c => c.catName)
                                .FirstOrDefault(), // Retrieve catName based on catId

                    itemSubCategory = _appDbContext.SubCategories
                                .Where(sc => sc.subCId == s.subCId)
                                .Select(sc => sc.subCatName)
                                .FirstOrDefault(), // Retrieve subCatName based on subCId
                })
                .ToList();

            ItemResponseModel response = new();
            response.itemData = list;
            response.pageSize = pageSize;
            response.pageCount = pageCount;
            response.pageNo = pageNo;
            return View("ItemIndex", response);
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

            item.itemCode = itemModel.itemCode;
            item.itemName = itemModel.itemName;
            item.itemPrice = itemModel.itemPrice;
            item.catId = itemModel.catId;
            item.subCId = itemModel.subCId;
            item.itemDescription = itemModel.itemDescription;
            item.itemBarcode = itemModel.itemBarcode;
            item.itemStock = itemModel.itemStock;
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
