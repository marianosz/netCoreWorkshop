using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApplication
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore().AddJsonFormatters();
        }

        // This method gets called by the runtime, after ConfigureServices, and is required. Use this method to configure the HTTP request pipeline.
        // IApplicationBuilder is required; provides the mechanisms to configure an application’s request pipeline.
        // Middleware is configured here.
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}