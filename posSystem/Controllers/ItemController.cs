using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using posSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace posSystem.Controllers
{
    public class ItemController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExcelService _fileUploadService;
        private readonly ILogger<ItemController> _logger;

        public ItemController(AppDbContext appDbContext, IExcelService fileUploadService, ILogger<ItemController> logger)
        {
            _appDbContext = appDbContext;
            _fileUploadService = fileUploadService;
            _logger = logger;
        }

        [HttpGet]
        [ActionName("MassUpload")]
        public IActionResult MassUpload()
        {
            bool hasData = _appDbContext.Items.Any();
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
                    var catItem = _appDbContext.Categories.FirstOrDefault(c => c.catCode == item.catCode);
                    var brandItem = _appDbContext.Brands.FirstOrDefault(c => c.brandCode == item.brandCode);
                    var catSubItem = _appDbContext.SubCategories.FirstOrDefault(c => c.subCatCode == item.subCatCode);
                    var subBrandItem = _appDbContext.SubBrands.FirstOrDefault(c => c.subBrandCode == item.subBrandCode);

                    if (catItem != null)
                        item.catId = catItem.catId;
                    if (catSubItem != null)
                        item.subCId = catSubItem.subCId;
                    if (subBrandItem != null)
                        item.subBId = subBrandItem.subBId;
                    if (brandItem != null)
                        item.brandId = brandItem.brandId;

                    item.itemCreateAt = DateTime.Now.ToString();
                    _appDbContext.Items.Add(item);
                }

                int result = _appDbContext.SaveChanges();

                rspModel = new MsgResopnseModel
                {
                    IsSuccess = result > 0,
                    responeMessage = result > 0 ? "Items saved successfully!" : "Failed to save items."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during mass upload.");
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
                var updateItems = _fileUploadService.ReadFromExcel<ItemModel>(file);

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
                        existingItem.itemStock += item.itemStock;
                        existingItem.itemSold += item.itemSold;
                        existingItem.itemRemainStock += item.itemStock;
                        existingItem.itemUpdateAt = DateTime.Now.ToString();
                        existingItem.itemUpdateCount = (existingItem.itemUpdateCount ?? 0) + 1;
                    }
                }

                int result = await _appDbContext.SaveChangesAsync();
                string message = result > 0 ? "Items updated successfully!" : "Failed to update items.";

                rspModel = new MsgResopnseModel
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during mass update.");
                rspModel = new MsgResopnseModel
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
                    case "itemcreateat":
                        query = ascending ? query.OrderBy(p => p.itemCreateAt) : query.OrderByDescending(p => p.itemCreateAt);
                        break;
                    case "itemupdateat":
                        query = ascending ? query.OrderBy(p => p.itemUpdateAt) : query.OrderByDescending(p => p.itemUpdateAt);
                        break;
                    default:
                        query = query.OrderBy(p => p.itemCode);
                        break;
                }

                var items = query.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

                return (items, pageCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during sorting and pagination.");
                throw;
            }
        }
    }
}
