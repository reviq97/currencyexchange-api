namespace currencyexchange_api.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = 500;
                _logger.LogCritical($"CODE:500, message {e.Message}");
                await context.Response.WriteAsync($"Something goes wrong: {e.Message}");
            }
        }
    }
}
