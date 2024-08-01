using ExcelReader.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ExcelReader.Services.Extensions;

public static class IServiceCollectionExtension
{
    public static void RegisterServices(this IServiceCollection services)
    {
        // Data.
        services.AddSingleton(typeof(IRepository<>), typeof(DapperRepository<>));
        services.AddScoped<IDataFileRepository, DataFileRepository>();

        // Services.
    }
}
