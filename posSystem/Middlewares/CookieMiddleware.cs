using Microsoft.EntityFrameworkCore;

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
                httpContext.Response.Redirect("/Login");
                goto result;
            }

            string adminId = cookies["AdminId"]!.ToString();
            string sessionId = cookies["SessionId"]!.ToString();

            var login = await appDbContext.LoginDetails.FirstOrDefaultAsync(x => x.sessionId == sessionId && x.adminId == adminId);
            if(login is null)
            {
                httpContext.Response.Redirect("/Login");
                goto result;
            }

            if(login.sessionExpired < DateTime.Now)
            {
                httpContext.Response.Redirect("/Login");
                goto result;
            }


            result:  
            await _next(httpContext);
        }
    }

    public static class CookieMiddlewareExtensions
    {
        public static IApplicationBuilder UseCookieMiddleware(
            this IApplicationBuilder buulder)
        {
            return buulder.UseMiddleware<CookieMiddleware>();
        }
    }
}
