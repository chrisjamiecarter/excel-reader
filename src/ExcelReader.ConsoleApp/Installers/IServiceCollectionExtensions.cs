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
        services.AddScoped<IDatabaseController, DatabaseController>();
        services.AddScoped<IWorkbookController, WorkbookController>();
        services.AddScoped<IWorksheetController, WorksheetController>();
        services.AddScoped<IColumnController, ColumnController>();
        services.AddScoped<IRowController, RowController>();
        services.AddScoped<ICellController, CellController>();

        // Data.
        services.AddSingleton(typeof(IRepository<>), typeof(SqliteRepository<>));
        services.AddScoped<ISqliteDatabaseRepository, SqliteDatabaseRepository>();
        services.AddTransient<IWorkbookRepository, WorkbookRepository>();
        services.AddTransient<IWorksheetRepository, WorksheetRepository>();
        services.AddTransient<IColumnRepository, ColumnRepository>();
        services.AddTransient<IRowRepository, RowRepository>();
        services.AddTransient<ICellRepository, CellRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
