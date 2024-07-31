using Microsoft.AspNetCore.Mvc;
using posSystem.Models;
using posSystem;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Xml;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        [ActionName("Detail")]
        public IActionResult DetailIndex(string invoiceNo, int pageNo = 1, int pageSize = 10)
        {
            try
            {
                var query = _appDbContext.SaleDetails.Where(x => x.invoiceNo == invoiceNo);
                int rowCount = query.Count();
                if (rowCount == 0)
                {
                    ModelState.AddModelError("", "No records found for the given invoice number.");
                    return View("DetailIndex", new SaleDetailResponseModel());
                }

                int pageCount = (int)Math.Ceiling((double)rowCount / pageSize);
                if (pageNo > pageCount)
                {
                    pageNo = pageCount;
                }

                List<SaleDetailModel> list = query
                .Include(s => s.Item)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new SaleDetailModel
                {
                    invoiceNo = s.invoiceNo,
                    itemCode = s.itemCode,
                    saleQuantity = s.saleQuantity,
                    itemPrice = s.itemPrice,
                    totalAmount = s.totalAmount,
                    saleDate = s.saleDate,
                    itemName = _appDbContext.Items
                        .Where(c => c.itemCode == s.itemCode)
                        .Select(c => c.itemName)
                        .FirstOrDefault(), // Retrieve catName based on catCod
                })
                .ToList();

                SaleDetailResponseModel response = new()
                {
                    saleDetailData = list,
                    pageSize = pageSize,
                    pageCount = pageCount,
                    pageNo = pageNo
                };

                return View("DetailIndex", response);
                //return View("DetailIndex");
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
