namespace ClientWeb.Helper.Middleware
{
    public class SessionAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();
            var userLogin = context.Session.GetString("UserLogin");
            var isAjax = context.Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            var allowPaths = new[]
            {
                "/user/signin",
                "/user/signup",
                "/user/checksignin",
                "/user/checkemailexist",
            };

            bool isAllowedPath = allowPaths.Any(p => path!.StartsWith(p));

            if (string.IsNullOrEmpty(userLogin) && !isAllowedPath)
            {
                if (isAjax)
                {
                    context.Response.StatusCode = 401;
                    return;
                }

                context.Response.Redirect("/User/SignIn");
                return;
            }

            await _next(context);
        }
    }
}
