using Microsoft.AspNetCore.Mvc;
using posSystem.Models;
using System.Text;
using System.Security.Cryptography;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore;

namespace posSystem.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public CategoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
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
            }

            List<CategoryModel> list = query
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (list, pageCount);
        }

        [ActionName("Index")]
        public IActionResult CategoryIndex(int pageNo = 1, int pageSize = 10, string sortField = "catName", string sortOrder = "asc")
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
            if (item.catUpdateCount is null)
            {
                item.catUpdateCount = 1;
            }
            else
            {
                item.catUpdateCount++;
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
