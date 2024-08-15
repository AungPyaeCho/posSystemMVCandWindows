using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using posSystem.Models;
using posSystem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace YourNamespace.Controllers
{
    public class PromotionController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<PromotionController> _logger;

        public PromotionController(AppDbContext appDbContext, ILogger<PromotionController> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        // GET action to display the list of promotions with sorting and pagination
        [ActionName("Index")]
        public IActionResult PromotionIndex(int pageNo = 1, int pageSize = 10, string sortField = "", string sortOrder = "asc")
        {
            try
            {
                var (list, pageCount) = GetSortedPromotions(pageNo, pageSize, sortField, sortOrder);

                var response = new PromotionResponseModel
                {
                    promotionData = list,
                    pageSize = pageSize,
                    pageCount = pageCount,
                    pageNo = pageNo,
                    sortField = sortField,
                    sortOrder = sortOrder
                };

                return View("PromotionIndex", response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving promotions.");
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return View("PromotionIndex");
            }
        }

        // GET action to show the promotion creation form
        [ActionName("Create")]
        public IActionResult PromotionCreate()
        {
            return View("PromotionCreate");
        }

        // POST action to save a new promotion
        [HttpPost]
        [ActionName("Save")]
        public IActionResult PromotionSave(PromotionModel promotionModel)
        {
            try
            {
                promotionModel.proCreateAt = DateTime.Now.ToString();
                _appDbContext.Promotions.Add(promotionModel);
                int result = _appDbContext.SaveChanges(); // Save changes to the database
                string message = result > 0 ? "Save Success" : "Save Fail";

                MsgResopnseModel rspModel = new MsgResopnseModel()
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };

                _logger.LogInformation("Promotion {PromotionName} saved successfully.", promotionModel.proName);
                return Json(rspModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving promotion.");
                MsgResopnseModel rspModel = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
                return Json(rspModel);
            }
        }

        // GET action to show the promotion editing form
        [ActionName("Edit")]
        public IActionResult PromotionEdit(int id)
        {
            try
            {
                var item = _appDbContext.Promotions.FirstOrDefault(x => x.proId == id);
                if (item == null)
                {
                    _logger.LogWarning("Edit attempt failed. Promotion with ID {Id} not found.", id);
                    return RedirectToAction("PromotionIndex");
                }
                return View("PromotionEdit", item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving promotion for editing with ID {Id}.", id);
                return RedirectToAction("PromotionIndex");
            }
        }

        // POST action to update an existing promotion
        [HttpPost]
        [ActionName("Update")]
        public IActionResult PromotionUpdate(int id, PromotionModel promotionModel)
        {
            var rspModel = new MsgResopnseModel();
            try
            {
                var item = _appDbContext.Promotions.FirstOrDefault(x => x.proId == id);
                if (item == null)
                {
                    _logger.LogWarning("Update attempt failed. Promotion with ID {Id} not found.", id);
                    rspModel = new MsgResopnseModel
                    {
                        IsSuccess = false,
                        responeMessage = "No data found"
                    };
                    return Json(rspModel);
                }

                // Update promotion details
                item.proName = promotionModel.proName;
                item.proCode = promotionModel.proCode;
                item.proDescription = promotionModel.proDescription;
                item.proUpdateAt = DateTime.Now.ToString();
                item.proUpdateCount ??= 0;
                item.proUpdateCount++;

                int result = _appDbContext.SaveChanges(); // Save changes to the database
                string message = result > 0 ? "Update Success" : "Update Fail";

                rspModel = new MsgResopnseModel
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };

                _logger.LogInformation("Promotion with ID {Id} updated successfully.", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating promotion with ID {Id}.", id);
                rspModel = new MsgResopnseModel
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
            }
            return Json(rspModel);
        }

        // POST action to delete a specific promotion
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult PromotionDelete(int id)
        {
            var rspModel = new MsgResopnseModel();
            try
            {
                var item = _appDbContext.Promotions.FirstOrDefault(x => x.proId == id);
                if (item == null)
                {
                    _logger.LogWarning("Delete attempt failed. Promotion with ID {Id} not found.", id);
                    rspModel = new MsgResopnseModel
                    {
                        IsSuccess = false,
                        responeMessage = "No data found"
                    };
                    return Json(rspModel);
                }

                _appDbContext.Promotions.Remove(item);
                int result = _appDbContext.SaveChanges(); // Save changes to the database
                string message = result > 0 ? "Delete Success" : "Delete Fail";

                rspModel = new MsgResopnseModel
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };

                _logger.LogInformation("Promotion with ID {Id} deleted successfully.", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting promotion with ID {Id}.", id);
                rspModel = new MsgResopnseModel
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
            }
            return Json(rspModel);
        }

        // POST action to delete all promotions
        [HttpPost]
        [ActionName("DeleteAll")]
        public IActionResult DeleteAllPromotion()
        {
            var rspModel = new MsgResopnseModel();

            try
            {
                var items = _appDbContext.Promotions.ToList();
                if (items.Count == 0)
                {
                    _logger.LogWarning("DeleteAll attempt failed. No promotions found to delete.");
                    rspModel = new MsgResopnseModel
                    {
                        IsSuccess = false,
                        responeMessage = "No promotions found to delete."
                    };
                    return Json(rspModel);
                }

                _appDbContext.Promotions.RemoveRange(items);
                int result = _appDbContext.SaveChanges(); // Save changes to the database
                string message = result > 0 ? "All promotions deleted successfully." : "Failed to delete promotions.";

                rspModel = new MsgResopnseModel
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };

                _logger.LogInformation("All promotions deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting all promotions.");
                rspModel = new MsgResopnseModel
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
            }

            return Json(rspModel);
        }

        // Helper method for sorting and pagination
        private (List<PromotionModel> list, int pageCount) GetSortedPromotions(int pageNo, int pageSize, string sortField, string sortOrder)
        {
            try
            {
                var query = _appDbContext.Promotions.AsQueryable();

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving sorted promotions.");
                throw; // Re-throw the exception to be handled by the calling method
            }
        }
    }
}
