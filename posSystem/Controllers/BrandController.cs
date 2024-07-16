using Microsoft.AspNetCore.Mvc;
using posSystem.Models;

namespace posSystem.Controllers
{
    public class BrandController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public BrandController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        private (List<BrandModel>, int) GetSorted(int pageNo, int pageSize, string sortField, string sortOrder)
        {
            int rowCount = _appDbContext.Brands.Count();
            int pageCount = rowCount / pageSize;

            if (rowCount % pageSize > 0)
                pageCount++;

            bool ascending = sortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase);

            IQueryable<BrandModel> query = _appDbContext.Brands.AsQueryable();

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

            List<BrandModel> list = query
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (list, pageCount);
        }


        [ActionName("Index")]
        public IActionResult BrandIndex(int pageNo = 1, int pageSize = 10, string sortField = "Id", string sortOrder = "asc")
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
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return View("BrandIndex");
            }
        }

        [ActionName("Create")]
        public IActionResult BrandCreate()
        {
            return View("BrandCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public IActionResult BrandSave(BrandModel brandModel)
        {

            brandModel.brandCreatedAt = DateTime.Now.ToString();
            _appDbContext.Brands.Add(brandModel);
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
        public IActionResult BrandEdit(int id)
        {
            var item = _appDbContext.Brands.FirstOrDefault(x => x.brandId == id);
            if (item is null)
            {
                return Redirect("/Brand");
            }
            return View("BrandEdit", item);
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult BrandUpdate(int id, BrandModel brandModel)
        {
            MsgResopnseModel rspModel = new MsgResopnseModel();
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

            item.brandName = brandModel.brandName;
            item.brandCode = brandModel.brandCode;
            item.brandDescription = brandModel.brandDescription;
            item.brandUpdatedAt = DateTime.Now.ToString();
            item.brandUpdateCount ??= 0;
            item.brandUpdateCount++;


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
        public IActionResult BrandDelete(int id)
        {
            MsgResopnseModel rspModel = new MsgResopnseModel();
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
