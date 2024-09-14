using DemoApp.Utilities.SecurityManagement;
using Microsoft.Extensions.Options;

namespace DemoApp.api.Middleware
{
    public class AccessMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<AccessOptions> _options;

        public AccessMiddleware(RequestDelegate next, IOptions<AccessOptions> options)
        {
            _next = next;
            _options = options;
        }
        public async Task Invoke(HttpContext context)
        {
            var xApiKey = context.Request.Headers["X-Api-Key"].FirstOrDefault();
            if (xApiKey != null && _options.Value.ApiKey == xApiKey)
            {
                context.Items["XApiKey"] = xApiKey;
                context.Items["UserRole"] = "admin";
            }

            await _next(context);
        }
    }
}
