using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ConsoleApplication
{
    public class Startup
    {
        // This method gets called by the runtime, after ConfigureServices, and is required. Use this method to configure the HTTP request pipeline.
        // IApplicationBuilder is required; provides the mechanisms to configure an application’s request pipeline.
        // Middleware is configured here.
        public void Configure(IApplicationBuilder app)
        {
            app.Run(context => context.Response.WriteAsync("Hello World, from ASP.NET!"));
        }
    }
}