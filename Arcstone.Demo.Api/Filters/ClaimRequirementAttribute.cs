using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Arcstone.Demo.Api.Filters
{
    public class ClaimRequirementAttribute : TypeFilterAttribute
    {
        public ClaimRequirementAttribute(params string[] role) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { role };
        }
    }

    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        private readonly string[] _claim;

        public ClaimRequirementFilter(string[] claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasClaim = context.HttpContext.User.Claims.Any(c => _claim.Contains(c.Value));
            if (!hasClaim)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}