using Arcstone.Demo.Application.Helpers;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Arcstone.Demo.Application.Extensions
{
    public static class HttpContextExtension
    {
        public static string GetUserId(this IHttpContextAccessor httpContextAccessor)
        {
            var userId = httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!userId.IsNullOrWhiteSpaceCustom())
            {
                return userId;
            }

            return string.Empty;
        }

        public static string GetRoleUser(this IHttpContextAccessor httpContextAccessor)
        {
            var userId = httpContextAccessor?.HttpContext?.User?.FindFirst("Roles")?.Value;
            if (!userId.IsNullOrWhiteSpaceCustom())
            {
                return userId;
            }

            return string.Empty;
        }

        public static string GetUserName(this IHttpContextAccessor httpContextAccessor)
        {
            var userName = httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            if (!userName.IsNullOrWhiteSpaceCustom())
            {
                return userName;
            }
            return "";
        }
    }
}