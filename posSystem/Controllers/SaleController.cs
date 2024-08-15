using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using posSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace posSystem.Controllers
{
    public class SaleController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<SaleController> _logger;

        public SaleController(AppDbContext appDbContext, ILogger<SaleController> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        // GET action to display the list of sales with sorting and pagination
        [ActionName("Index")]
        public IActionResult SaleIndex(int pageNo = 1, int pageSize = 10, string sortField = "", string sortOrder = "asc")
        {
            try
            {
                _logger.LogInformation("Fetching sales data for page {PageNo}, page size {PageSize}, sort field {SortField}, sort order {SortOrder}.", pageNo, pageSize, sortField, sortOrder);

                var (list, pageCount) = GetSortedSales(pageNo, pageSize, sortField, sortOrder);

                var response = new SaleResponseModel
                {
                    saleData = list,
                    pageSize = pageSize,
                    pageCount = pageCount,
                    pageNo = pageNo
                };

                _logger.LogInformation("Successfully fetched {RecordCount} sales records.", list.Count);
                return View("SaleIndex", response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching sales data for page {PageNo}.", pageNo);
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return View("SaleIndex");
            }
        }

        [ActionName("Detail")]
        public IActionResult DetailIndex(string invoiceNo, int pageNo = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching sale details for invoice number {InvoiceNo}, page {PageNo}, page size {PageSize}.", invoiceNo, pageNo, pageSize);

                var query = _appDbContext.SaleDetails.Where(x => x.invoiceNo == invoiceNo);
                int rowCount = query.Count();
                if (rowCount == 0)
                {
                    _logger.LogWarning("No records found for invoice number {InvoiceNo}.", invoiceNo);
                    ModelState.AddModelError("", "No records found for the given invoice number.");
                    return View("DetailIndex", new SaleDetailResponseModel());
                }

                int pageCount = (int)Math.Ceiling((double)rowCount / pageSize);
                if (pageNo > pageCount)
                {
                    pageNo = pageCount;
                }

                var list = query
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
                            .FirstOrDefault(),
                    })
                    .ToList();

                var response = new SaleDetailResponseModel
                {
                    saleDetailData = list,
                    pageSize = pageSize,
                    pageCount = pageCount,
                    pageNo = pageNo
                };

                _logger.LogInformation("Successfully fetched {RecordCount} sale details for invoice number {InvoiceNo}.", list.Count, invoiceNo);
                return View("DetailIndex", response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching sale details for invoice number {InvoiceNo}.", invoiceNo);
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return View("DetailIndex");
            }
        }

        private (List<SaleModel> sales, int pageCount) GetSortedSales(int pageNo, int pageSize, string sortField, string sortOrder)
        {
            try
            {
                _logger.LogInformation("Sorting sales data by {SortField} in {SortOrder} order.", sortField, sortOrder);

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
                    .Select(s => new SaleModel
                    {
                        invoiceNo = s.invoiceNo,
                        staffCode = s.staffCode,
                        memberCode = s.memberCode,
                        saleQty = s.saleQty,
                        totalAmount = s.totalAmount,
                        receiveCash = s.receiveCash,
                        refundCash = s.refundCash,
                        paymentMethod = s.paymentMethod,
                        promotion = s.promotion,
                        discount = s.discount,
                        saleDate = s.saleDate,
                        memberName = _appDbContext.Members
                            .Where(c => c.memberCode == s.memberCode)
                            .Select(c => c.memberName)
                            .FirstOrDefault(),
                        staffName = _appDbContext.Staffs
                            .Where(c => c.staffCode == s.staffCode)
                            .Select(c => c.staffName)
                            .FirstOrDefault(),
                    })
                    .ToList();

                _logger.LogInformation("Successfully fetched {RecordCount} sales records for page {PageNo}.", sales.Count, pageNo);
                return (sales, pageCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while sorting and paginating sales data.");
                throw; // Rethrow the exception to be handled by the caller
            }
        }
    }
}
