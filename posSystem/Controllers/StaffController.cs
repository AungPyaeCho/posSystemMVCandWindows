using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using posSystem.Models;

namespace posSystem.Controllers
{
    public class StaffController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StaffController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        [ActionName("Index")]
        public IActionResult StaffIndex(int pageNo = 1, int pageSize = 10)
        {
            int rowCount = _appDbContext.Staffs.Count();
            int pageCount = rowCount / pageSize;

            if (rowCount % pageSize > 0)
                pageCount++;

            List<StaffModel> list = _appDbContext.Staffs
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            //Decrypt the passwords before returning
            foreach (var Staff in list)
            {
                Staff.staffPassword = Staff.GetDecryptedPassword();
            }

            StaffResponseModel response = new()
            {
                staffData = list,
                pageSize = pageSize,
                pageCount = pageCount,
                pageNo = pageNo
            };
            return View("StaffIndex", response);
        }

        [ActionName("Create")]
        public IActionResult StaffCreate()
        {
            return View("StaffCreate");
        }

        [HttpPost]
        [ActionName("Check")]
        public JsonResult CheckEmail(string email)
        {
            bool isEmailRegistered = _appDbContext.Staffs.Any(a => a.staffEmail == email);
            return Json(new { isEmailRegistered });
        }

        [HttpPost]
        [ActionName("Save")]
        public IActionResult StaffSave(StaffModel staffModel,IFormFile staffPhoto)
        {
            if (ModelState.IsValid)
            {
                if (_appDbContext.Staffs.Any(a => a.staffEmail == staffModel.staffEmail))
                {
                    var emailExistsResponse = new MsgResopnseModel
                    {
                        IsSuccess = false,
                        responeMessage = "This email is already registered."
                    };
                    return Json(emailExistsResponse);
                }

                staffModel.SetEncryptedPassword(staffModel.staffPassword);

                if (staffPhoto != null && staffPhoto.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "staffimages");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var uniqueFileName = staffModel.staffName + "_" + Path.GetFileName(staffPhoto.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        staffPhoto.CopyTo(fileStream);
                    }

                    staffModel.staffPhoto = "/staffimages/" + uniqueFileName;
                }
                staffModel.staffCreateAt = DateTime.Now.ToString();


                _appDbContext.Staffs.Add(staffModel);
                int result = _appDbContext.SaveChanges();

                string message = result > 0 ? "Save Success" : "Save Fail";
                var model = new MsgResopnseModel
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };

                return Json(model);
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return BadRequest(errors);
        }

        [ActionName("Edit")]
        public IActionResult StaffEdit(int staffId)
        {
            var item = _appDbContext.Staffs.FirstOrDefault(x => x.staffId == staffId);
            if (item is null)
            {
                return Redirect("/Staff");
            }

            return View("StaffEdit", item);
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult StaffUpdate(StaffModel staffModel, IFormFile staffPhoto, bool updatePhoto)
        {
            MsgResopnseModel msg = new MsgResopnseModel();
            var item = _appDbContext.Staffs.FirstOrDefault(x => x.staffId == staffModel.staffId);
            if (item == null)
            {
                msg = new MsgResopnseModel()
                {
                    IsSuccess = false,
                    responeMessage = "No data found"
                };
                return Json(msg);
            }

            item.staffCode = staffModel.staffCode;
            item.staffName = staffModel.staffName;
            item.staffRole = staffModel.staffRole;
            item.staffPhone = staffModel.staffPhone;
            item.staffAddress = staffModel.staffAddress;
            item.staffJoinedDate = staffModel.staffJoinedDate;
            item.staffEmail = staffModel.staffEmail;

            if (updatePhoto)
            {
                // Delete the old photo if it exists
                if (!string.IsNullOrEmpty(item.staffPhoto))
                {
                    var oldPhotoPath = Path.Combine(_webHostEnvironment.WebRootPath, item.staffPhoto.TrimStart('/'));
                    if (System.IO.File.Exists(oldPhotoPath))
                    {
                        System.IO.File.Delete(oldPhotoPath);
                    }
                }

                // Save the new photo
                if (staffPhoto != null && staffPhoto.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "staffimages");
                    var uniqueFileName = staffModel.staffName + "_" + Path.GetFileName(staffPhoto.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        staffPhoto.CopyTo(fileStream);
                    }

                    item.staffPhoto = "/staffimages/" + uniqueFileName;
                }
            }

            int result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Update Success" : "Update Fail";
            msg = new MsgResopnseModel()
            {
                IsSuccess = result > 0,
                responeMessage = message
            };

            return Json(msg);
        }


        [HttpPost]
        [ActionName("Delete")]
        public IActionResult StaffDelete(int staffId)
        {
            MsgResopnseModel msg = new MsgResopnseModel();
            var item = _appDbContext.Staffs.FirstOrDefault(x => x.staffId == staffId);
            if (item is null)
            {
                msg.IsSuccess = false;
                msg.responeMessage = "No data found";
                return Json(msg);
            }

            if (!string.IsNullOrEmpty(item.staffPhoto))
            {
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, item.staffPhoto.TrimStart('~', '/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _appDbContext.Remove(item);
            int result = _appDbContext.SaveChanges();
            msg.IsSuccess = result > 0;
            msg.responeMessage = msg.IsSuccess ? "Delete Success" : "Delete Fail";
            return Json(msg);
        }
    }
}
