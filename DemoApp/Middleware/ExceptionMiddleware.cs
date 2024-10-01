using System.Text;

namespace DemoApp.api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var message = "General error";
            var bytes = Encoding.UTF8.GetBytes(message);
            var statusCode = StatusCodes.Status500InternalServerError;
            try
            {
                if (!context.Request.Body.CanSeek)
                {
                    context.Request.EnableBuffering();
                }

                await _next(context);
            }
            catch (UnauthorizedAccessException ex)
            {
                statusCode = StatusCodes.Status401Unauthorized;
                message = "Unauthorized access";
                bytes = Encoding.UTF8.GetBytes(message);

                context.Response.StatusCode = statusCode;
                _logger.LogError(ex, $"{statusCode} - {message}");
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = statusCode;
                _logger.LogError(ex, $"{statusCode} - {message}");
            }
        }
    }
}
