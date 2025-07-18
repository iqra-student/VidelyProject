namespace WebApplication3.Middleware
{
    public class RoleBasedAccessMiddleware
    {
        private readonly RequestDelegate _next;

        public RoleBasedAccessMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.ToString().ToLower();

            // Allow public pages
            if (path == "/" || path.Contains("/account/login") || path.Contains("/account/register") || path.Contains("/account/logout"))
            {
                await _next(context);
                return;
            }


            var role = context.Session.GetString("Role");

            // If no role found, redirect to login
            if (string.IsNullOrEmpty(role))
            {
                context.Response.Redirect("/Account/Login");
                return;
            }

            // Customer allowed pages
            if (role == "Customer")
            {
                if (path.Contains("/movies/simplemovies") || path.Contains("/rentals/new"))
                {
                    await _next(context);
                    return;
                }
                else
                {
                    // Customer trying to access Admin page
                    context.Response.Redirect("/Movies/SimpleMovies");
                    return;
                }
            }

            // Admin allowed pages
            if (role == "Admin")
            {
                if (path.Contains("/movies/random") || path.Contains("/customers") || path.Contains("/movies"))
                {
                    await _next(context);
                    return;
                }
                else
                {
                    // Admin trying to access Customer page
                    context.Response.Redirect("/Movies/Random");
                    return;
                }
            }

            await _next(context);
        }
    }
}
