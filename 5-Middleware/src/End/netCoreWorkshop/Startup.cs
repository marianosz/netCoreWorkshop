using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using netCoreWorkshop.Business;
using Microsoft.EntityFrameworkCore;
using netCoreWorkshop.Middlewares; 

namespace netCoreWorkshop
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder() // Collection of sources for read/write key/value pairs
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables(); // Overrides environment variables with valiues from config files/etc

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Setup options with DI
            services.AddOptions();
            
            services.Configure<APIKeyOptions>(Configuration.GetSection("APIKeyOptions"));
            
            services.AddDbContext<Data.ArticlesContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("Articles"));
            });

            services.AddTransient<IArticlesService, ArticlesService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var startupLogger = loggerFactory.CreateLogger<Startup>();

            startupLogger.LogTrace("Trace test output!");
            startupLogger.LogDebug("Debug test output!");
            startupLogger.LogInformation("Info test output!");            
            startupLogger.LogError("Error test output!");
            startupLogger.LogCritical("Trace test output!");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            
            app.UseAPIKey();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}