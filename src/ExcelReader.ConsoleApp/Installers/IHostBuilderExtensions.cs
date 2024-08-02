using ExcelReader.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ExcelReader.ConsoleApp.Installers;

/// <summary>
/// Microsoft.Extensions.Hosting.IHostBuilder interface extension methods.
/// </summary>
public static class IHostBuilderExtensions
{
    /// <summary>
    /// Gets the installers for this application and performs the InstallServices method on each.
    /// </summary>
    /// <param name="builder">The IHostBuilder.</param>
    public static void AddDependancies(this IHostBuilder builder)
    {
        builder.ConfigureServices((hostContext, services) =>
        {
            services.RegisterServices();
            
            // Configure Logging.
            services.AddSerilog((service, config) => config.ReadFrom.Configuration(hostContext.Configuration).Enrich.FromLogContext().WriteTo.Console());

            // Configure Options.
            services.AddOptions<ApplicationOptions>().Bind(hostContext.Configuration.GetSection(nameof(ApplicationOptions)));
        });
    }
}
