using Microsoft.AspNetCore.Mvc;
using posSystem.Models;
using posSystem;

namespace posSystem.Controllers
{
    public class MemberController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MemberController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
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
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return View("MemberIndex");
            }
        }

        // GET action to show the member creation form
        [ActionName("Create")]
        public IActionResult MemberCreate()
        {
            return View("MemberCreate");
        }

        [HttpPost]
        [ActionName("Check")]
        public JsonResult CheckEmail(string email)
        {
            try
            {
                bool isEmailRegistered = _appDbContext.Members.Any(a => a.memberEmail == email);
                return Json(new { isEmailRegistered });
            }
            catch (Exception ex)
            {
               
                return Json(new { isEmailRegistered = false });
            }
        }

        // POST action to save a new member
        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> MemberSave(MemberModel memberModel, IFormFile memberPhoto)
        {
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

                    var rspModel = new MsgResopnseModel()
                    {
                        IsSuccess = result > 0,
                        responeMessage = message
                    };
                    return Json(rspModel);
                }

                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(errors);
            }
            catch (Exception ex)
            {
                var rspModel = new MsgResopnseModel()
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
            }
            catch (Exception ex)
            {
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
            }
            catch (Exception ex)
            {
                rspModel = new MsgResopnseModel
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
            }
            return Json(rspModel);
        }

        // POST action to delete all members
        [HttpPost]
        [ActionName("DeleteAll")]
        public async Task<IActionResult> DeleteAllMembers()
        {
            var rspModel = new MsgResopnseModel();

            try
            {
                var items = _appDbContext.Members.ToList();
                if (items.Count == 0)
                {
                    rspModel = new MsgResopnseModel
                    {
                        IsSuccess = false,
                        responeMessage = "No members found to delete."
                    };
                    return Json(rspModel);
                }

                // Delete photos of all members
                foreach (var item in items)
                {
                    if (!string.IsNullOrEmpty(item.memberPhoto))
                    {
                        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, item.memberPhoto.TrimStart('/'));
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }
                }

                _appDbContext.Members.RemoveRange(items);
                int result = await _appDbContext.SaveChangesAsync();
                string message = result > 0 ? "All members deleted successfully." : "Failed to delete members.";

                rspModel = new MsgResopnseModel
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };
            }
            catch (Exception ex)
            {
                rspModel = new MsgResopnseModel
                {
                    IsSuccess = false,
                    responeMessage = $"An error occurred: {ex.Message}"
                };
            }
            return Json(rspModel);
        }

        private (List<MemberModel> members, int pageCount) GetSortedMembers(int pageNo, int pageSize, string sortField, string sortOrder)
        {
            var query = _appDbContext.Members.AsQueryable();

            // Sorting
            if (!string.IsNullOrEmpty(sortField))
            {
                switch (sortField.ToLower())
                {
                    case "name":
                        query = sortOrder.ToLower() == "desc" ? query.OrderByDescending(x => x.memberName) : query.OrderBy(x => x.memberName);
                        break;
                    case "email":
                        query = sortOrder.ToLower() == "desc" ? query.OrderByDescending(x => x.memberEmail) : query.OrderBy(x => x.memberEmail);
                        break;
                    case "phone":
                        query = sortOrder.ToLower() == "desc" ? query.OrderByDescending(x => x.memberPhone) : query.OrderBy(x => x.memberPhone);
                        break;
                    // Add other cases if needed
                    default:
                        query = query.OrderBy(x => x.memberName);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(x => x.memberName);
            }

            var pageCount = (int)Math.Ceiling((double)query.Count() / pageSize);

            // Pagination
            var members = query
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (members, pageCount);
        }
    }
}
