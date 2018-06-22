using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace netCoreWorkshop
{
	class Program
	{
		// Minimal ASP.NET setup in Main method
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args)
		{
			var commandLineConfig = new ConfigurationBuilder()  
				.AddCommandLine(args)
				.Build();

			return WebHost.CreateDefaultBuilder(args)
				.UseConfiguration(commandLineConfig)
				.ConfigureAppConfiguration((builderContext, config) =>
				{
					IHostingEnvironment env = builderContext.HostingEnvironment;

					config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
						.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

					config.AddEnvironmentVariables();
				})
				.ConfigureLogging((hostingContext, logging) =>
				{
					logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
					logging.AddConsole();
					logging.AddDebug();
				})
				.UseStartup<Startup>();
		}
	}
}
