using Microsoft.AspNetCore.Mvc;
using posSystem.Models;
using System.Text;
using System.Security.Cryptography;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore;

namespace posSystem.Controllers
{
    public class SubCategoryController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public SubCategoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [ActionName("Index")]
        public IActionResult SubCategoryIndex(int pageNo = 1, int pageSize = 10)
        {
            int rowCount = _appDbContext.SubCategories.Count();
            int pageCount = rowCount / pageSize;

            if (rowCount % pageSize > 0)
                pageCount++;

            List<SubCategoryModel> list = _appDbContext.SubCategories
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
                                .Where(c => c.catId == s.catId)
                                .Select(c => c.catName)
                                .FirstOrDefault(), // Retrieve catName based on catCod
                })
                .ToList();

            SubCategoryResponseModel response = new();
            response.subCategoryData = list;
            response.pageSize = pageSize;
            response.pageCount = pageCount;
            response.pageNo = pageNo;
            return View("SubCategoryIndex", response);
        }

        [ActionName("Create")]
        public IActionResult SubCategoryCreate()
        {
            var categories = _appDbContext.Categories.ToList();
            ViewBag.Categories = categories;
            return View("SubCategoryCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public IActionResult SubCategorySave(SubCategoryModel subCategoryModel)
        {
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

        [ActionName("Edit")]
        public IActionResult SubCategoryEdit(int subCId)
        {
            Console.WriteLine($"Received catId: {subCId}");
            var item = _appDbContext.SubCategories.FirstOrDefault(x => x.subCId == subCId);
            Console.WriteLine($"Received catId: {subCId}");
            if (item is null)
            {
                return Redirect("/SubCategory");
            }

            var categories = _appDbContext.Categories.ToList();
            ViewBag.Categories = categories;

            return View("SubCategoryEdit", item);

            //Console.WriteLine($"Received catId: {subCId}");

            //// Fetch the subcategory item based on subCId
            //var item = _appDbContext.SubCategories.FirstOrDefault(x => x.subCId == subCId);

            //if (item is null)
            //{
            //    return Redirect("/SubCategory");
            //}

            //// Fetch the catName based on subCId
            //var catName = (from subCat in _appDbContext.SubCategories
            //               join cat in _appDbContext.Categories on subCat.catId equals cat.catId
            //               where subCat.subCId == subCId
            //               select cat.catName).FirstOrDefault();

            //// Add the fetched catName to the item
            //item.catName = catName;

            //// Fetch all categories to pass to the view
            //var categories = _appDbContext.Categories.ToList();
            //ViewBag.Categories = categories;

            //return View("SubCategoryEdit", item);
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult SubCategoryUpdate(int subCId, SubCategoryModel subCategoryModel)
        {
            Console.WriteLine($"Received catId: {subCId}");
            MsgResopnseModel rspModel = new MsgResopnseModel();
            var item = _appDbContext.SubCategories.FirstOrDefault(x => x.subCId == subCId);
            if (item is null)
            {
                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = "No data found"
                };
                return Json(rspModel);
            }

            item.subCatName = subCategoryModel.subCatName;
            item.subCatCode = subCategoryModel.subCatCode;
            item.catCode = subCategoryModel.catCode;
            item.subCatUpdateAt = DateTime.Now.ToString();
            if (item.subCatUpdateCount is null)
            {
                item.subCatUpdateCount = 1;
            }
            else
            {
                item.subCatUpdateCount++;
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
        public IActionResult SubCategoryDelete(int subCId)
        {
            MsgResopnseModel rspModel = new MsgResopnseModel();
            var item = _appDbContext.SubCategories.FirstOrDefault(x => x.subCId == subCId);
            if (item is null)
            {
                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = "No data found"
                };
                return Json(rspModel);
            }

            _appDbContext.SubCategories.Remove(item);
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
