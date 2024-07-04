using Microsoft.AspNetCore.Mvc;
using posSystem.Models;
using System.Text;
using System.Security.Cryptography;
using Microsoft.VisualBasic;

namespace posSystem.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public CategoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [ActionName("Index")]
        public IActionResult CategoryIndex(int pageNo = 1, int pageSize = 10)
        {
            int rowCount = _appDbContext.Categories.Count();
            int pageCount = rowCount / pageSize;

            if (rowCount % pageSize > 0)
                pageCount++;

            List<CategoryModel> list = _appDbContext.Categories
                //.OrderByDescending(x => x.id)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            CategoryResponseModel response = new();
            response.categoryData = list;
            response.pageSize = pageSize;
            response.pageCount = pageCount;
            response.pageNo = pageNo;
            return View("CategoryIndex", response);
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

        [ActionName("Create")]
        public IActionResult CategoryCreate()
        {
            return View("CategoryCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public IActionResult CategorySave(CategoryModel categoryModel)
        {
            
            categoryModel.catCreatedAt = DateTime.UtcNow;
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

            int result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Update Success" : "Update Fail";
            rspModel = new MsgResopnseModel()
            {
                IsSuccess = result > 0,
                responeMessage = message
            };
            return Json(rspModel);
        }

        //[HttpPost]
        //[ActionName("Delete")]
        //public IActionResult CategoryDelete(CategoryModel categoryModel)
        //{
        //    MsgResopnseModel rspModel = new MsgResopnseModel();
        //    var item = _appDbContext.Categories.FirstOrDefault(x => x.catId == categoryModel.catId);
        //    if (item is null)
        //    {
        //        rspModel = new MsgResopnseModel()
        //        {
        //            IsSuccess = false,
        //            responeMessage = "No data found"
        //        };
        //        return Json(rspModel);
        //    }

        //    _appDbContext.Remove(item);
        //    int result = _appDbContext.SaveChanges();
        //    string message = result > 0 ? "Delete Success" : "Delete Fail";
        //    rspModel = new MsgResopnseModel()
        //    {
        //        IsSuccess = result > 0,
        //        responeMessage = message
        //    };
        //    return Json(rspModel);
        //}

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
    }
}
