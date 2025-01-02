using System.Net;

namespace DailyReport.EndpointFilters
{
    public class LogNotFoundResponseFilter(ILogger<LogNotFoundResponseFilter> logger) : IEndpointFilter
    {
        private readonly ILogger<LogNotFoundResponseFilter> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var result = await next(context);
            var actualResult = (result is INestedHttpResult) ? ((INestedHttpResult)result).Result : (IResult)result;

            if ((actualResult as IStatusCodeHttpResult)?.StatusCode == (int)HttpStatusCode.NotFound)
            {
                _logger.LogInformation($"Resource {context.HttpContext.Request.Path} was not found.");
            }

            return result;
        }
    }
}
