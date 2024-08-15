using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

namespace posSystem.Middlewares
{
    public class CookieMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CookieMiddleware> _logger;

        public CookieMiddleware(RequestDelegate next, ILogger<CookieMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext, AppDbContext appDbContext)
        {
            try
            {
                string requestUrl = httpContext.Request.Path.ToString().ToLower();
                if (requestUrl == "/login" || requestUrl == "/login/index")
                {
                    await _next(httpContext);
                    return;
                }

                var cookies = httpContext.Request.Cookies;
                if (cookies["AdminId"] == null || cookies["SessionId"] == null)
                {
                    _logger.LogWarning("AdminId or SessionId cookie is null");
                    httpContext.Response.Redirect("/Login");
                    return;
                }

                string adminId = cookies["AdminId"]!;
                string sessionId = cookies["SessionId"]!;
                string adminName = cookies["AdminName"]!;

                var login = await appDbContext.LoginDetails
                    .FirstOrDefaultAsync(x => x.sessionId == sessionId && x.adminId == adminId);

                if (login == null)
                {
                    _logger.LogWarning("No login found for given sessionId and adminId");
                    httpContext.Response.Redirect("/Login");
                    return;
                }

                if (login.sessionExpired < DateTime.Now)
                {
                    _logger.LogWarning("Session expired");
                    httpContext.Response.Redirect("/Login");
                    return;
                }

                // Continue processing the request
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in CookieMiddleware");
                httpContext.Response.Redirect("/Error");
            }
        }
    }

    public static class CookieMiddlewareExtensions
    {
        public static IApplicationBuilder UseCookieMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CookieMiddleware>();
        }
    }
}
