using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoApp.Core.Utils.Security.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.FilterDescriptors.Any(fd => fd.Filter is AllowAnonymousAttribute);
            if(allowAnonymous)
                return;

            var xApiKey = context.HttpContext.Items["XApiKey"]?.ToString();
            if(xApiKey == null)
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
