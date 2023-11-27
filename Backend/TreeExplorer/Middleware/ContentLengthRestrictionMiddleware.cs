using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TreeExplorer.Models;

namespace TreeExplorer.Middleware
{
    public class ContentLengthRestrictionMiddleware
    {
        private readonly ContentLengthRestrictionOptions _contentLengthRestrictionOptions;
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<ContentLengthRestrictionMiddleware> _logger;

        public ContentLengthRestrictionMiddleware(RequestDelegate nextRequestDelegate, ContentLengthRestrictionOptions contentLengthRestrictionOptions, ILoggerFactory loggerFactory)
        {
            _requestDelegate = nextRequestDelegate;
            _contentLengthRestrictionOptions = contentLengthRestrictionOptions;
            _logger = loggerFactory.CreateLogger<ContentLengthRestrictionMiddleware>();
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (_contentLengthRestrictionOptions != null && _contentLengthRestrictionOptions.ContentLengthLimit > 0 && httpContext.Request.ContentLength > _contentLengthRestrictionOptions.ContentLengthLimit)
            {
                _logger.LogWarning("Rejecting request with Content-Length {0} more then allowed {1}.", httpContext.Request.ContentLength, _contentLengthRestrictionOptions.ContentLengthLimit);
                httpContext.Response.StatusCode = StatusCodes.Status413RequestEntityTooLarge;
                await httpContext.Response.WriteAsJsonAsync(new
                {
                    Title = "Request too large",
                    Status = StatusCodes.Status413RequestEntityTooLarge
                });
                await httpContext.Response.CompleteAsync();
            }
            else
            {
                await _requestDelegate.Invoke(httpContext);
            }
        }
    }
}
