using ExcelReader.ConsoleApp.Controllers;
using ExcelReader.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ExcelReader.ConsoleApp.Installers;

public static class IServiceCollectionExtension
{
    public static void RegisterServices(this IServiceCollection services)
    {
        // App.
        services.AddHostedService<App>();
        services.AddScoped<IDataFileController, DataFileController>();

        // Data.
        services.AddSingleton(typeof(IRepository<>), typeof(SqliteRepository<>));
        services.AddScoped<IDataFileRepository, DataFileRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
