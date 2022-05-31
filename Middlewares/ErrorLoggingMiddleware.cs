using System;

using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace cw_8_22c.Middlewares
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public ErrorLoggingMiddleware()
        {
        }

        public async Task Invoke(HttpContext context)
        {
            //await File.AppendAllTextAsync("logs.txt", "test");
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Unexpected error.");
                await File.AppendAllTextAsync("logs.txt", e.InnerException.ToString() + '\n');
            }
        }
    }
}
