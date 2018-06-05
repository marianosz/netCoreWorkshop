using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace netCoreWorkshop.Middlewares
{
    public class APIKeyMiddleware
    {
        private readonly RequestDelegate next;
        private readonly APIKeyOptions options;

        public APIKeyMiddleware(RequestDelegate next, IOptions<APIKeyOptions> options)
        {
            this.next = next;
            this.options = options.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value.StartsWith("/api"))
            {
                var apiKey = "asdfgh";

                if (options?.APIKeyValue != null)
                {
                    apiKey = options.APIKeyValue;
                }

                if (!context.Request.Headers.ContainsKey("api-key") ||
                    context.Request.Headers["api-key"] != apiKey)
                {
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync("Authentication required to execute this request");

                    return;
                }
            }

            // Call the next delegate/middleware in the pipeline
            await this.next(context);
        }
    }

    public static class APIKeyMiddlewareExtensions
    {
        public static IApplicationBuilder UseAPIKey(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<APIKeyMiddleware>();
        }
    }
}