using Microsoft.AspNetCore.Mvc;
using posSystem.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace posSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public LoginController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AdminModel adminModel, bool rememberMe)
        {
            adminModel.SetEncryptedPassword(adminModel.adminPassword);
            var encPsw = adminModel.adminPassword;
            var item = _appDbContext.Admin.FirstOrDefault(x => x.adminEmail == adminModel.adminEmail && x.adminPassword == encPsw);
            if (item is null) return View();

            string sessionId = Guid.NewGuid().ToString();
            DateTime sessionExpired = rememberMe ? DateTime.Now.AddDays(30) : DateTime.Now.AddMinutes(360);

            CookieOptions cookie = new CookieOptions();
            cookie.Expires = sessionExpired;
            Response.Cookies.Append("AdminId", item.id, cookie);
            Response.Cookies.Append("SessionId", sessionId, cookie);

            await _appDbContext.LoginDetails.AddAsync(new LoginDetailModel
            {
                ldId = Guid.NewGuid().ToString(),
                adminId = item.id,
                adminEmail = item.adminEmail,
                sessionId = sessionId,
                sessionExpired = sessionExpired,
                loginAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            });
            await _appDbContext.SaveChangesAsync();
            return Redirect("/Home");
        }
    }
}
