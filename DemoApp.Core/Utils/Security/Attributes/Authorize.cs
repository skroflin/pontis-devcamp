using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DemoApp.Core.Utils.Security.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _roles;

        public AuthorizeAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            var allowAnonymous = context.ActionDescriptor.FilterDescriptors.Any(fd => fd.Filter is AllowAnonymousAttribute);
            if (allowAnonymous)
                return;

            var xApiKey = context.HttpContext.Items["XApiKey"]?.ToString();
            if (xApiKey == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userRole = context.HttpContext.Items["UserRole"]?.ToString();

            if (_roles == null || _roles.Length == 0)
            {
                return;
            }

            if (userRole == null || !_roles.Contains(userRole))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
