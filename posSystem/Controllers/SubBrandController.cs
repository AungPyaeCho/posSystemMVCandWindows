using Microsoft.AspNetCore.Mvc;
using posSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace posSystem.Controllers
{
    public class SubBrandController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public SubBrandController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        private (List<SubBrandModel>, int) GetSorted(int pageNo, int pageSize, string sortField, string sortOrder)
        {
            int rowCount = _appDbContext.SubBrands.Count();
            int pageCount = rowCount / pageSize;

            if (rowCount % pageSize > 0)
                pageCount++;

            bool ascending = sortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase);

            IQueryable<SubBrandModel> query = _appDbContext.SubBrands.AsQueryable();

            switch (sortField.ToLower())
            {
                case "subcatname":
                    query = ascending ? query.OrderBy(p => p.subBrandName) : query.OrderByDescending(p => p.subBrandName);
                    break;
                case "subcatcode":
                    query = ascending ? query.OrderBy(p => p.subBrandCode) : query.OrderByDescending(p => p.subBrandCode);
                    break;
                default:
                    query = ascending ? query.OrderBy(p => p.subBId) : query.OrderByDescending(p => p.subBId);
                    break;
            }

            List<SubBrandModel> list = query
                .Include(s => s.Brand)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new SubBrandModel
                {
                    subBId = s.subBId,
                    subBrandName = s.subBrandName,
                    subBrandCode = s.subBrandCode,
                    subBrandCreateAt = s.subBrandCreateAt,
                    subBrandUpdateAt = s.subBrandUpdateAt,
                    subBrandUpdateCount = s.subBrandUpdateCount,
                    brandName = _appDbContext.Brands
                        .Where(c => c.brandCode == s.brandCode || c.brandId == s.brandId)
                        .Select(c => c.brandName)
                        .FirstOrDefault(),
                })
                .ToList();
            return (list, pageCount);
        }


        [ActionName("Index")]
        public IActionResult SubBrandIndex(int pageNo = 1, int pageSize = 10, string sortField = "", string sortOrder = "asc")
        {
            try
            {
                var (list, pageCount) = GetSorted(pageNo, pageSize, sortField, sortOrder);

                SubBrandResponseModel response = new()
                {
                    subBrandData = list,
                    pageSize = pageSize,
                    pageCount = pageCount,
                    pageNo = pageNo,
                    sortField = sortField,
                    sortOrder = sortOrder
                };

                return View("SubBrandIndex", response);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return View("SubBrandIndex");
            }
        }

        [ActionName("Create")]
        public IActionResult SubBrandCreate()
        {
            var brands = _appDbContext.Brands.ToList();
            ViewBag.Brands = brands;
            return View("SubBrandCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public IActionResult SubBrandSave(SubBrandModel subBrandModel)
        {
            var brandItem = _appDbContext.Brands.FirstOrDefault(c => c.brandId == subBrandModel.brandId)!;
            subBrandModel.brandCode = brandItem.brandCode;
            subBrandModel.subBrandCreateAt = DateTime.Now.ToString();
            _appDbContext.SubBrands.Add(subBrandModel);
            int result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Save Success" : "Save Fail";
            MsgResopnseModel rspModel = new MsgResopnseModel()
            {
                IsSuccess = result > 0,
                responeMessage = message
            }!;
            return Json(rspModel);
        }

        [ActionName("Edit")]
        public IActionResult SubBrandEdit(int subBId)
        {
            var item = _appDbContext.SubBrands.FirstOrDefault(x => x.subBId == subBId);
            if (item is null)
            {
                return Redirect("/SubBrand");
            }

            var brands = _appDbContext.Brands.ToList();
            ViewBag.Brands = brands;

            return View("SubBrandEdit", item);
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult SubBrandUpdate(int subBId, SubBrandModel subBrandModel)
        {
            MsgResopnseModel rspModel = new MsgResopnseModel()!;
            var item = _appDbContext.SubBrands.FirstOrDefault(x => x.subBId == subBId);
            if (item is null)
            {
                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = "No data found"
                };
                return Json(rspModel);
            }

            var catItem = _appDbContext.SubBrands.FirstOrDefault(c => c.subBId == subBrandModel.subBId)!;
            item.subBrandName = subBrandModel.subBrandName;
            item.subBrandCode = subBrandModel.subBrandCode;
            item.brandId = subBrandModel.brandId;
            item.brandCode = subBrandModel.brandCode;
            item.subBrandUpdateAt = DateTime.Now.ToString();
            item.subBrandUpdateCount ??= 0;
            item.subBrandUpdateCount++;

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
        public IActionResult SubBrandDelete(int subBId)
        {
            MsgResopnseModel rspModel = new MsgResopnseModel();
            var item = _appDbContext.SubBrands.FirstOrDefault(x => x.subBId == subBId);
            if (item is null)
            {
                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = "No data found"
                };
                return Json(rspModel);
            }

            _appDbContext.SubBrands.Remove(item);
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
