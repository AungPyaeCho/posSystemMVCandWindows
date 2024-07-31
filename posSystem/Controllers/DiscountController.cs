using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using posSystem.Models;
using posSystem;
using System;
using System.Linq;

namespace YourNamespace.Controllers
{
    public class DiscountController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public DiscountController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
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
                // Handle errors during data retrieval and sorting
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
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
                int result = _appDbContext.SaveChanges(); // Save changes to the database
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
                // Handle errors during the save operation
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
            var item = _appDbContext.Discounts.FirstOrDefault(x => x.disId == id);
            if (item is null)
            {
                return Redirect("/Discount");
            }
            return View("DiscountEdit", item);
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
                if (item is null)
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

                int result = _appDbContext.SaveChanges(); // Save changes to the database
                string message = result > 0 ? "Update Success" : "Update Fail";

                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };
            }
            catch (Exception ex)
            {
                // Handle errors during the update operation
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
                if (item is null)
                {
                    rspModel = new MsgResopnseModel()
                    {
                        IsSuccess = false,
                        responeMessage = "No data found"
                    };
                    return Json(rspModel);
                }

                _appDbContext.Discounts.Remove(item);
                int result = _appDbContext.SaveChanges(); // Save changes to the database
                string message = result > 0 ? "Delete Success" : "Delete Fail";

                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };
            }
            catch (Exception ex)
            {
                // Handle errors during the delete operation
                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
            }
            return Json(rspModel);
        }

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
                        responeMessage = "No brands found to delete."
                    };
                    return Json(rspModel);
                }

                _appDbContext.Discounts.RemoveRange(items);
                int result = _appDbContext.SaveChanges(); // Save changes to the database
                string message = result > 0 ? "All discounts deleted successfully." : "Failed to delete discounts.";

                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };
            }
            catch (Exception ex)
            {
                // Handle errors during the delete all operation
                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
            }

            return Json(rspModel);
        }

        // Helper method for sorting and pagination (implement according to your needs)
        private (List<DiscountModel> list, int pageCount) GetSortedDiscounts(int pageNo, int pageSize, string sortField, string sortOrder)
        {
            // Implement your sorting and pagination logic here
            // This is a placeholder implementation
            var query = _appDbContext.Discounts.AsQueryable();

            if (!string.IsNullOrEmpty(sortField))
            {
                // Implement sorting logic here
                query = sortOrder == "asc" ? query.OrderBy(x => EF.Property<object>(x, sortField)) : query.OrderByDescending(x => EF.Property<object>(x, sortField));
            }

            int totalCount = query.Count();
            int pageCount = (int)Math.Ceiling(totalCount / (double)pageSize);
            var list = query.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

            return (list, pageCount);
        }
    }
}