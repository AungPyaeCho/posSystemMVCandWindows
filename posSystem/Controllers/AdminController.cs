using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Security.Cryptography;
using posSystem.Models;
using System;


namespace posSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public AdminController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [ActionName("Index")]
        public IActionResult AdminIndex(int pageNo = 1, int pageSize = 10)
        {
            int rowCount = _appDbContext.Admin.Count();
            int pageCount = rowCount / pageSize;

            if (rowCount % pageSize > 0)
                pageCount++;

            List<AdminModel> list = _appDbContext.Admin
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

        [ActionName("Create")]
        public IActionResult AdminCreate()
        {
            return View("AdminCreate");
        }

        [HttpPost]
        [ActionName("Check")]
        public JsonResult CheckEmail(string email)
        {
            bool isEmailRegistered = _appDbContext.Admin.Any(a => a.adminEmail == email);
            return Json(new { isEmailRegistered });
        }

        [HttpPost]
        [ActionName("Save")]
        public IActionResult AdminSave(AdminModel adminModel)
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

        [ActionName("Edit")]
        public IActionResult AdminEdit(string id)
        {
            var item = _appDbContext.Admin.FirstOrDefault(x => x.id == id);
            if (item is null)
            {
                return Redirect("/Admin");
            }

            item.adminPassword = item.GetDecryptedPassword();
            return View("AdminEdit", item);
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult AdminUpdate(AdminModel adminModel)
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

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult AdminDelete(string id)
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
    }
}
