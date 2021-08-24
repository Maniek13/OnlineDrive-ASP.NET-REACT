using Microsoft.AspNetCore.Builder;
using TreeExplorer.Objects;

namespace TreeExplorer.Middleware
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseContentLengthRestriction(this IApplicationBuilder builder, ContentLengthRestrictionOptions contentLengthRestrictionOptions)
            => builder.UseMiddleware<ContentLengthRestrictionMiddleware>(contentLengthRestrictionOptions);
    }
}
