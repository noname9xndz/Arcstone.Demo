using Arcstone.Demo.Client.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Arcstone.Demo.Client.Middlewares
{
    public class CheckAuthenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ConfigAudience _appSettings;

        public CheckAuthenMiddleware(RequestDelegate next, IOptions<ConfigAudience> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated && httpContext.Request.Path.Value != "/Account/Login")
            {
                httpContext.Response.Redirect("/Account/Login");
            }

            await _next(httpContext);
        }
    }
}