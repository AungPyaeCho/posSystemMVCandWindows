using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using Azure.Core;

namespace posSystem.Middlewares
{
    public class CookieMiddleware
    {
        private readonly RequestDelegate _next;

        public CookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, AppDbContext appDbContext)
        {
            string requestUrl = httpContext.Request.Path.ToString().ToLower();
            if (requestUrl == "/login" || requestUrl == "/login/index")
                goto result;

            var cookies = httpContext.Request.Cookies;
            if (cookies["AdminId"] is null || cookies["SessionId"] is null)
            {
                Console.WriteLine("AdminId or SessionId cookie is null");
                httpContext.Response.Redirect("/Login");
                goto result;
            }

            string adminId = cookies["AdminId"]!;
            string sessionId = cookies["SessionId"]!;
            string adminName = cookies["AdminName"]!;

            var login = await appDbContext.LoginDetails.FirstOrDefaultAsync(x => x.sessionId == sessionId && x.adminId == adminId);
            if (login is null)
            {
                Console.WriteLine("No login found for given sessionId and adminId");
                httpContext.Response.Redirect("/Login");
                goto result;
            }

            if (login.sessionExpired < DateTime.Now)
            {
                Console.WriteLine("Session expired");
                httpContext.Response.Redirect("/Login");
                goto result;
            }

        result:
            await _next(httpContext);
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
