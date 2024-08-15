using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using posSystem.Models;
using posSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace posSystem.Controllers
{
    public class MemberController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<MemberController> _logger;

        public MemberController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment, ILogger<MemberController> logger)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        // GET action to display the list of members with sorting and pagination
        [ActionName("Index")]
        public IActionResult MemberIndex(int pageNo = 1, int pageSize = 10, string sortField = "", string sortOrder = "asc")
        {
            try
            {
                var (list, pageCount) = GetSortedMembers(pageNo, pageSize, sortField, sortOrder);

                var response = new MemberResponseModel
                {
                    memberData = list,
                    pageSize = pageSize,
                    pageCount = pageCount,
                    pageNo = pageNo,
                    sortField = sortField,
                    sortOrder = sortOrder
                };

                return View("MemberIndex", response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving members.");
                ModelState.AddModelError("", "An error occurred while retrieving members.");
                return View("MemberIndex");
            }
        }

        // GET action to show the member creation form
        [ActionName("Create")]
        public IActionResult MemberCreate()
        {
            return View("MemberCreate");
        }

        // POST action to save a new member
        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> MemberSave(MemberModel memberModel, IFormFile memberPhoto)
        {
            var rspModel = new MsgResopnseModel();
            try
            {
                if (ModelState.IsValid)
                {
                    if (memberPhoto != null && memberPhoto.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "memberimages");

                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        var uniqueFileName = memberModel.memberName + "_" + Path.GetFileName(memberPhoto.FileName);
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await memberPhoto.CopyToAsync(fileStream);
                        }

                        memberModel.memberPhoto = "/memberimages/" + uniqueFileName;
                    }
                    memberModel.SetEncryptedPassword(memberModel.memberPassword);
                    memberModel.memberCreateAt = DateTime.Now.ToString();
                    _appDbContext.Members.Add(memberModel);
                    int result = await _appDbContext.SaveChangesAsync(); // Save changes to the database
                    string message = result > 0 ? "Save Success" : "Save Fail";

                    rspModel = new MsgResopnseModel()
                    {
                        IsSuccess = result > 0,
                        responeMessage = message
                    };

                    _logger.LogInformation("Member {MemberName} saved successfully.", memberModel.memberName);
                    return Json(rspModel);
                }

                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                _logger.LogWarning("Member save failed. Validation errors: {Errors}", string.Join(", ", errors));
                return BadRequest(errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving member.");
                rspModel = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
                return Json(rspModel);
            }
        }

        // GET action to show the member editing form
        [ActionName("Edit")]
        public IActionResult MemberEdit(int id)
        {
            var item = _appDbContext.Members.FirstOrDefault(x => x.memberId == id);
            if (item == null)
            {
                _logger.LogWarning("Edit attempt failed. Member with ID {Id} not found.", id);
                return RedirectToAction("MemberIndex");
            }
            return View("MemberEdit", item);
        }

        // POST action to update an existing member
        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> MemberUpdate(int id, MemberModel memberModel, IFormFile memberPhoto, bool updatePhoto)
        {
            var rspModel = new MsgResopnseModel();
            try
            {
                var item = _appDbContext.Members.FirstOrDefault(x => x.memberId == id);
                if (item == null)
                {
                    _logger.LogWarning("Update attempt failed. Member with ID {Id} not found.", id);
                    rspModel = new MsgResopnseModel
                    {
                        IsSuccess = false,
                        responeMessage = "No data found"
                    };
                    return Json(rspModel);
                }

                item.memberCode = memberModel.memberCode;
                item.memberName = memberModel.memberName;
                item.memberEmail = memberModel.memberEmail;
                item.memberPhone = memberModel.memberPhone;
                item.memberAddress = memberModel.memberAddress;
                item.memberLevel = memberModel.memberLevel;
                item.memberPoints = memberModel.memberPoints;
                item.memberUsedPoints = memberModel.memberUsedPoints;
                item.memberUpdateAt = DateTime.Now.ToString();
                item.memberUpdateCount ??= 0;
                item.memberUpdateCount++;

                if (updatePhoto)
                {
                    // Delete the old photo if it exists
                    if (!string.IsNullOrEmpty(item.memberPhoto))
                    {
                        var oldPhotoPath = Path.Combine(_webHostEnvironment.WebRootPath, item.memberPhoto.TrimStart('/'));
                        if (System.IO.File.Exists(oldPhotoPath))
                        {
                            System.IO.File.Delete(oldPhotoPath);
                        }
                    }

                    // Save the new photo
                    if (memberPhoto != null && memberPhoto.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "memberimages");
                        var uniqueFileName = memberModel.memberName + "_" + Path.GetFileName(memberPhoto.FileName);
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await memberPhoto.CopyToAsync(fileStream);
                        }

                        item.memberPhoto = "/memberimages/" + uniqueFileName;
                    }
                }

                int result = await _appDbContext.SaveChangesAsync();
                string message = result > 0 ? "Update Success" : "Update Fail";
                rspModel = new MsgResopnseModel
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };

                _logger.LogInformation("Member {MemberName} updated successfully.", memberModel.memberName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating member with ID {Id}.", id);
                rspModel = new MsgResopnseModel
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
            }
            return Json(rspModel);
        }

        // POST action to delete a specific member
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> MemberDelete(int id)
        {
            var rspModel = new MsgResopnseModel();
            try
            {
                var item = _appDbContext.Members.FirstOrDefault(x => x.memberId == id);
                if (item == null)
                {
                    _logger.LogWarning("Delete attempt failed. Member with ID {Id} not found.", id);
                    rspModel = new MsgResopnseModel
                    {
                        IsSuccess = false,
                        responeMessage = "No data found"
                    };
                    return Json(rspModel);
                }

                // Delete the photo if it exists
                if (!string.IsNullOrEmpty(item.memberPhoto))
                {
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, item.memberPhoto.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                _appDbContext.Members.Remove(item);
                int result = await _appDbContext.SaveChangesAsync();
                string message = result > 0 ? "Delete Success" : "Delete Fail";

                rspModel = new MsgResopnseModel
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };

                _logger.LogInformation("Member with ID {Id} deleted successfully.", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting member with ID {Id}.", id);
                rspModel = new MsgResopnseModel
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
            }
            return Json(rspModel);
        }

        // Helper method to get sorted members
        private (List<MemberModel> list, int pageCount) GetSortedMembers(int pageNo, int pageSize, string sortField, string sortOrder)
        {
            var members = _appDbContext.Members.AsQueryable();

            // Sorting
            if (sortField == "memberName" && sortOrder == "asc")
                members = members.OrderBy(m => m.memberName);
            else if (sortField == "memberName" && sortOrder == "desc")
                members = members.OrderByDescending(m => m.memberName);

            // Pagination
            var totalRecords = members.Count();
            var pageCount = (int)Math.Ceiling((double)totalRecords / pageSize);
            var list = members.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

            return (list, pageCount);
        }
    }
}
