using Microsoft.AspNetCore.Mvc;
using posSystem.Models;
using posSystem;

namespace posSystem.Controllers
{
    public class SaleController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public SaleController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // GET action to display the list of sales with sorting and pagination
        [ActionName("Index")]
        public IActionResult SaleIndex(int pageNo = 1, int pageSize = 10, string sortField = "", string sortOrder = "asc")
        {
            try
            {
                var (list, pageCount) = GetSortedSales(pageNo, pageSize, sortField, sortOrder);

                var response = new SaleResponseModel
                {
                    saleData = list,
                    pageSize = pageSize,
                    pageCount = pageCount,
                    pageNo = pageNo
                };

                return View("SaleIndex", response);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return View("SaleIndex");
            }
        }

        private (List<SaleModel> sales, int pageCount) GetSortedSales(int pageNo, int pageSize, string sortField, string sortOrder)
        {
            var query = _appDbContext.Sales.AsQueryable();

            // Sorting
            if (!string.IsNullOrEmpty(sortField))
            {
                switch (sortField.ToLower())
                {
                    case "saleid":
                        query = sortOrder.ToLower() == "desc" ? query.OrderByDescending(x => x.saleId) : query.OrderBy(x => x.saleId);
                        break;
                    case "staffid":
                        query = sortOrder.ToLower() == "desc" ? query.OrderByDescending(x => x.staffId) : query.OrderBy(x => x.staffId);
                        break;
                    case "memberid":
                        query = sortOrder.ToLower() == "desc" ? query.OrderByDescending(x => x.memberId) : query.OrderBy(x => x.memberId);
                        break;
                    case "totalamount":
                        query = sortOrder.ToLower() == "desc" ? query.OrderByDescending(x => x.totalAmount) : query.OrderBy(x => x.totalAmount);
                        break;
                    case "saledate":
                        query = sortOrder.ToLower() == "desc" ? query.OrderByDescending(x => x.saleDate) : query.OrderBy(x => x.saleDate);
                        break;
                    // Add other cases if needed
                    default:
                        query = query.OrderBy(x => x.saleId);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(x => x.saleId);
            }

            var pageCount = (int)Math.Ceiling((double)query.Count() / pageSize);

            // Pagination
            var sales = query
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (sales, pageCount);
        }
    }
}
