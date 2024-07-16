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
            var item = _appDbContext.Admin.FirstOrDefault(x => x.adminEmail == adminModel.adminEmail && x.adminPassword == encPsw)!;
            if (item is null) return View();

            string sessionId = Guid.NewGuid().ToString();
            DateTime sessionExpired = rememberMe ? DateTime.Now.AddDays(30) : DateTime.Now.AddMinutes(60);

            CookieOptions cookieOptions = new CookieOptions
            {
                Expires = sessionExpired,
                IsEssential = true, // Ensure cookies are essential for session management
                SameSite = SameSiteMode.Strict,
                HttpOnly = true,
                Secure = true, // Ensure cookies are sent only over HTTPS if available
            };

            Response.Cookies.Append("AdminId", item.id, cookieOptions);
            Response.Cookies.Append("SessionId", sessionId, cookieOptions);
            Response.Cookies.Append("AdminName", item.adminName, cookieOptions);

            HttpContext.Session.SetString("AdminId", item.id);
            HttpContext.Session.SetString("AdminName", item.adminName);



            await _appDbContext.LoginDetails.AddAsync(new LoginDetailModel
            {
                ldId = Guid.NewGuid().ToString(),
                adminId = item.id,
                adminEmail = item.adminEmail,
                adminName = item.adminName,
                sessionId = sessionId,
                sessionExpired = sessionExpired,
                loginAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            });

            await _appDbContext.SaveChangesAsync();
            return Redirect("/Home");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            // Clear cookies when logging out
            Response.Cookies.Delete("AdminId");
            Response.Cookies.Delete("SessionId");

            // Optionally, clear session data or log out from identity system if used

            return RedirectToAction("Index", "Login"); // Redirect to login page after logout
        }
    }
}
