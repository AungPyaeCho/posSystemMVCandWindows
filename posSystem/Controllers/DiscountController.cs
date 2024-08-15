using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using posSystem;
using posSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace YourNamespace.Controllers
{
    public class DiscountController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<DiscountController> _logger;

        public DiscountController(AppDbContext appDbContext, ILogger<DiscountController> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        // GET action to display the list of discounts with sorting and pagination
        [ActionName("Index")]
        public IActionResult DiscountIndex(int pageNo = 1, int pageSize = 10, string sortField = "", string sortOrder = "asc")
        {
            try
            {
                var (list, pageCount) = GetSortedDiscounts(pageNo, pageSize, sortField, sortOrder);

                DiscountResponseModel response = new()
                {
                    discountData = list,
                    pageSize = pageSize,
                    pageCount = pageCount,
                    pageNo = pageNo,
                    sortField = sortField,
                    sortOrder = sortOrder
                };

                return View("DiscountIndex", response);
            }
            catch (Exception ex)
            {
                // Log the error and handle it
                _logger.LogError(ex, "Error occurred while retrieving the discount list.");
                ModelState.AddModelError("", "An error occurred while retrieving the discounts. Please try again later.");
                return View("DiscountIndex");
            }
        }

        // GET action to show the discount creation form
        [ActionName("Create")]
        public IActionResult DiscountCreate()
        {
            return View("DiscountCreate");
        }

        // POST action to save a new discount
        [HttpPost]
        [ActionName("Save")]
        public IActionResult DiscountSave(DiscountModel discountModel)
        {
            try
            {
                discountModel.disCreateAt = DateTime.Now.ToString();
                _appDbContext.Discounts.Add(discountModel);
                int result = _appDbContext.SaveChanges();
                string message = result > 0 ? "Save Success" : "Save Fail";

                MsgResopnseModel rspModel = new MsgResopnseModel()
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };

                return Json(rspModel);
            }
            catch (Exception ex)
            {
                // Log the error and handle it
                _logger.LogError(ex, "Error occurred while saving the discount.");
                MsgResopnseModel rspModel = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
                return Json(rspModel);
            }
        }

        // GET action to show the discount editing form
        [ActionName("Edit")]
        public IActionResult DiscountEdit(int id)
        {
            try
            {
                var item = _appDbContext.Discounts.FirstOrDefault(x => x.disId == id);
                if (item == null)
                {
                    _logger.LogWarning("No discount found with ID {DiscountId}", id);
                    return RedirectToAction("Index");
                }
                return View("DiscountEdit", item);
            }
            catch (Exception ex)
            {
                // Log the error and handle it
                _logger.LogError(ex, "Error occurred while retrieving the discount for editing.");
                return RedirectToAction("Index");
            }
        }

        // POST action to update an existing discount
        [HttpPost]
        [ActionName("Update")]
        public IActionResult DiscountUpdate(int id, DiscountModel discountModel)
        {
            MsgResopnseModel rspModel = new MsgResopnseModel();
            try
            {
                var item = _appDbContext.Discounts.FirstOrDefault(x => x.disId == id);
                if (item == null)
                {
                    rspModel = new MsgResopnseModel()
                    {
                        IsSuccess = false,
                        responeMessage = "No data found"
                    };
                    return Json(rspModel);
                }

                // Update discount details
                item.disName = discountModel.disName;
                item.disValue = discountModel.disValue;
                item.disDescription = discountModel.disDescription;
                item.disUpdateAt = DateTime.Now.ToString();
                item.disUpdateCount ??= 0;
                item.disUpdateCount++;

                int result = _appDbContext.SaveChanges();
                string message = result > 0 ? "Update Success" : "Update Fail";

                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };
            }
            catch (Exception ex)
            {
                // Log the error and handle it
                _logger.LogError(ex, "Error occurred while updating the discount.");
                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
            }
            return Json(rspModel);
        }

        // POST action to delete a specific discount
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DiscountDelete(int id)
        {
            MsgResopnseModel rspModel = new MsgResopnseModel();
            try
            {
                var item = _appDbContext.Discounts.FirstOrDefault(x => x.disId == id);
                if (item == null)
                {
                    rspModel = new MsgResopnseModel()
                    {
                        IsSuccess = false,
                        responeMessage = "No data found"
                    };
                    return Json(rspModel);
                }

                _appDbContext.Discounts.Remove(item);
                int result = _appDbContext.SaveChanges();
                string message = result > 0 ? "Delete Success" : "Delete Fail";

                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };
            }
            catch (Exception ex)
            {
                // Log the error and handle it
                _logger.LogError(ex, "Error occurred while deleting the discount.");
                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
            }
            return Json(rspModel);
        }

        // POST action to delete all discounts
        [HttpPost]
        [ActionName("DeleteAll")]
        public IActionResult DeleteAllDiscount()
        {
            MsgResopnseModel rspModel = new MsgResopnseModel();

            try
            {
                var items = _appDbContext.Discounts.ToList();
                if (items.Count == 0)
                {
                    rspModel = new MsgResopnseModel()
                    {
                        IsSuccess = false,
                        responeMessage = "No discounts found to delete."
                    };
                    return Json(rspModel);
                }

                _appDbContext.Discounts.RemoveRange(items);
                int result = _appDbContext.SaveChanges();
                string message = result > 0 ? "All discounts deleted successfully." : "Failed to delete discounts.";

                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };
            }
            catch (Exception ex)
            {
                // Log the error and handle it
                _logger.LogError(ex, "Error occurred while deleting all discounts.");
                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
            }

            return Json(rspModel);
        }

        // Helper method for sorting and pagination
        private (List<DiscountModel> list, int pageCount) GetSortedDiscounts(int pageNo, int pageSize, string sortField, string sortOrder)
        {
            try
            {
                var query = _appDbContext.Discounts.AsQueryable();

                if (!string.IsNullOrEmpty(sortField))
                {
                    // Implement sorting logic
                    query = sortOrder == "asc" ? query.OrderBy(x => EF.Property<object>(x, sortField)) : query.OrderByDescending(x => EF.Property<object>(x, sortField));
                }

                int totalCount = query.Count();
                int pageCount = (int)Math.Ceiling(totalCount / (double)pageSize);
                var list = query.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

                return (list, pageCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while sorting and paginating discounts.");
                throw; // Rethrow the exception to be handled by the calling method
            }
        }
    }
}
