using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using posSystem.Models;

namespace posSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<AdminController> _logger;

        public AdminController(AppDbContext appDbContext, ILogger<AdminController> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        [ActionName("Index")]
        public IActionResult AdminIndex(int pageNo = 1, int pageSize = 10)
        {
            try
            {
                // Replace 'defaultAdminId' with the actual Id or any other unique identifier for the default admin
                var defaultAdmin = "Default Admin"; // Example: Assuming the default admin has Id = 1

                int rowCount = _appDbContext.Admin
                    .Count(a => a.adminName != defaultAdmin);

                int pageCount = rowCount / pageSize;

                if (rowCount % pageSize > 0)
                    pageCount++;

                List<AdminModel> list = _appDbContext.Admin
                    .Where(a => a.adminName != defaultAdmin) // Exclude default admin
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                // Decrypt the passwords before returning
                foreach (var admin in list)
                {
                    admin.adminPassword = admin.GetDecryptedPassword();
                }

                AdminResponseModel response = new()
                {
                    adminData = list,
                    pageSize = pageSize,
                    pageCount = pageCount,
                    pageNo = pageNo
                };

                return View("AdminIndex", response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching admin data.");
                return StatusCode(500, "Internal server error.");
            }
        }


        [ActionName("Create")]
        public IActionResult AdminCreate()
        {
            return View("AdminCreate");
        }

        [HttpPost]
        [ActionName("Check")]
        public JsonResult CheckEmail(string email)
        {
            try
            {
                bool isEmailRegistered = _appDbContext.Admin.Any(a => a.adminEmail == email);
                return Json(new { isEmailRegistered });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking email.");
                return Json(500, "Internal server error.");
            }
        }

        [HttpPost]
        [ActionName("Save")]
        public IActionResult AdminSave(AdminModel adminModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_appDbContext.Admin.Any(a => a.adminEmail == adminModel.adminEmail))
                    {
                        var emailExistsResponse = new MsgResopnseModel
                        {
                            IsSuccess = false,
                            responeMessage = "This email is already registered."
                        };
                        return Json(emailExistsResponse);
                    }

                    if (string.IsNullOrEmpty(adminModel.id))
                    {
                        adminModel.id = Guid.NewGuid().ToString();
                    }
                    adminModel.adminCreateAt = DateTime.Now.ToString();
                    adminModel.SetEncryptedPassword(adminModel.adminPassword);

                    _appDbContext.Admin.Add(adminModel);
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving admin data.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [ActionName("Edit")]
        public IActionResult AdminEdit(string id)
        {
            try
            {
                var item = _appDbContext.Admin.FirstOrDefault(x => x.id == id);
                if (item is null)
                {
                    return Redirect("/Admin");
                }

                item.adminPassword = item.GetDecryptedPassword();
                return View("AdminEdit", item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching admin data for edit.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult AdminUpdate(AdminModel adminModel)
        {
            try
            {
                MsgResopnseModel msg = new MsgResopnseModel();
                var item = _appDbContext.Admin.FirstOrDefault(x => x.id == adminModel.id);
                if (item is null)
                {
                    msg = new MsgResopnseModel()
                    {
                        IsSuccess = false,
                        responeMessage = "No data found"
                    };
                    return Json(msg);
                }

                item.adminName = adminModel.adminName;
                item.adminEmail = adminModel.adminEmail;
                //item.adminPassword = adminModel.adminPassword;
                //item.SetEncryptedPassword(adminModel.adminPassword);

                int result = _appDbContext.SaveChanges();
                string message = result > 0 ? "Update Success" : "Update Fail";
                msg = new MsgResopnseModel()
                {
                    IsSuccess = result > 0,
                    responeMessage = message
                };

                return Json(msg);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating admin data.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult AdminDelete(string id)
        {
            try
            {
                MsgResopnseModel msg = new MsgResopnseModel();
                var item = _appDbContext.Admin.FirstOrDefault(x => x.id == id);
                if (item is null)
                {
                    msg.IsSuccess = false;
                    msg.responeMessage = "No data found";
                    return Json(msg);
                }

                _appDbContext.Remove(item);
                int result = _appDbContext.SaveChanges();
                msg.IsSuccess = result > 0;
                msg.responeMessage = msg.IsSuccess ? "Delete Success" : "Delete Fail";
                return Json(msg);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting admin data.");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
