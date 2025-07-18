using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebApplication3.Middleware
{
    public class SessionCheckMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context?.Request?.Path.Value?.ToLower();

            // Allow these URLs without login
            var allowedPaths = new[]
 {
    "/account/login",
    "/account/register",
    "/account/logout",   // ✅ Add this line
    "/css", "/js", "/images",
    "/favicon.ico"
};

            bool isAllowed = allowedPaths.Any(p => path.StartsWith(p));

            if (!isAllowed)
            {
                var userEmail = context?.Session.GetString("UserEmail");

                if (string.IsNullOrEmpty(userEmail))
                {
                    // Not logged in -> Redirect to Login
                    context?.Response.Redirect("/Account/Login");
                    return;
                }
            }

            await _next(context);
        }
    }
}
