using ExcelReader.Data.Repositories;
using ExcelReader.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ExcelReader.ConsoleApp.Installers;

public static class IServiceCollectionExtension
{
    public static void RegisterServices(this IServiceCollection services)
    {
        // App.
        services.AddHostedService<App>();

        // Data.
        services.AddSingleton(typeof(IRepository<>), typeof(SqliteRepository<>));
        services.AddScoped<ISqliteDatabaseRepository, SqliteDatabaseRepository>();
        services.AddScoped<IWorkbookRepository, WorkbookRepository>();
        services.AddScoped<IWorksheetRepository, WorksheetRepository>();
        services.AddScoped<IColumnRepository, ColumnRepository>();
        services.AddScoped<IRowRepository, RowRepository>();
        services.AddScoped<ICellRepository, CellRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Service.
        services.AddScoped<IDataFileProcessor, DataFileProcessor>();
        services.AddScoped<IDataFileReader, DataFileReader>();
        services.AddScoped<ICsvDataFileReader, CsvDataFileReader>();
        services.AddScoped<IExcelDataFileReader, ExcelDataFileReader>();
        services.AddScoped<IDatabaseService, DatabaseService>();
    }
}
