using cw_8_22c.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace cw_8_22c.Extensions
{
    public static class ErrorLoggingMiddlewareExtensions
    {

        public static IApplicationBuilder UseErrorLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorLoggingMiddleware>();
        }
    }
}
