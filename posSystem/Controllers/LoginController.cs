using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<LoginController> _logger;

        public LoginController(AppDbContext appDbContext, ILogger<LoginController> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AdminModel adminModel, bool rememberMe)
        {
            try
            {
                adminModel.SetEncryptedPassword(adminModel.adminPassword);
                var encPsw = adminModel.adminPassword;

                var item = _appDbContext.Admin.FirstOrDefault(x => x.adminEmail == adminModel.adminEmail && x.adminPassword == encPsw);
                if (item == null)
                {
                    _logger.LogWarning("Login attempt failed for email: {Email}", adminModel.adminEmail);
                    return View();
                }

                string sessionId = Guid.NewGuid().ToString();
                DateTime sessionExpired = DateTime.Now.AddMonths(1);

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
                _logger.LogInformation("User {AdminName} logged in successfully", item.adminName);

                return Redirect("/Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login attempt for email: {Email}", adminModel.adminEmail);
                // Optionally, you might want to return an error view or a friendly message
                return View("Error"); // Ensure you have an Error view to display the message
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            try
            {
                // Clear cookies when logging out
                Response.Cookies.Delete("AdminId");
                Response.Cookies.Delete("SessionId");
                Response.Cookies.Delete("AdminName");

                // Optionally, clear session data or log out from identity system if used
                HttpContext.Session.Clear();

                _logger.LogInformation("User logged out successfully");

                return RedirectToAction("Index", "Login"); // Redirect to login page after logout
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during logout");
                // Optionally, you might want to return an error view or a friendly message
                return View("Error"); // Ensure you have an Error view to display the message
            }
        }
    }
}
