using ExcelReader.ConsoleApp.Controllers;
using ExcelReader.ConsoleApp.Engines;
using ExcelReader.Models;
using ExcelReader.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Spectre.Console;

namespace ExcelReader.ConsoleApp;

/// <summary>
/// A ConsoleApplication implemented as a HostedService.
/// </summary>
internal class App : IHostedService
{
    #region Fields

    private readonly IHostApplicationLifetime _appLifetime;
    private readonly ILogger<App> _logger;
    private readonly IDatabaseController _databaseController;
    private readonly IWorkbookController _workbookController;
    private readonly IWorksheetController _worksheetController;
    private readonly IColumnController _columnController;
    private readonly IRowController _rowController;
    private readonly ICellController _cellController;
    private int? _exitCode;

    #endregion
    #region Constructors

    public App(IHostApplicationLifetime appLifetime, 
        ILogger<App> logger, 
        IDatabaseController databaseController, 
        IWorkbookController workbookController, 
        IWorksheetController worksheetController,
        IColumnController columnController,
        IRowController rowController,
        ICellController cellController)
    {
        _appLifetime = appLifetime;
        _logger = logger;
        _databaseController = databaseController;
        _workbookController = workbookController;
        _worksheetController = worksheetController;
        _columnController = columnController;
        _rowController = rowController;
        _cellController = cellController;
    }

    #endregion
    #region Methods
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _appLifetime.ApplicationStarted.Register(() =>
        {
            Task.Run(async () =>
            {
                try
                {
                    _logger.LogInformation("Application started");

                    _logger.LogInformation("Resetting database");
                    _databaseController.Reset();
                    _logger.LogInformation("Database reset");

                    var inputFilePath = Path.GetFullPath("..\\..\\..\\..\\..\\_test\\Incoming\\");
                    var reader = new ExcelReaderService();

                    _logger.LogInformation("Processing incoming directory {directory}", inputFilePath);
                    var workbooks = reader.Process(inputFilePath);
                    _logger.LogInformation("Incoming directory processed");

                    _logger.LogInformation("Adding data to database");
                    _logger.LogInformation("Workbooks: {count}", workbooks.Count);
                    _logger.LogInformation("Worksheets: {count}", workbooks.Sum(x => x.Worksheets.Count));
                    _logger.LogInformation("Columns: {count}", workbooks.Sum(x => x.Worksheets.Sum(y => y.Columns.Count)));
                    _logger.LogInformation("Rows: {count}", workbooks.Sum(x => x.Worksheets.Sum(y => y.Rows.Count)));
                    foreach (var workbook in workbooks)
                    {
                        workbook.Id = await _workbookController.CreateAsync(workbook);
                        foreach (var worksheet in workbook.Worksheets)
                        {
                            worksheet.WorkbookId = workbook.Id;
                            worksheet.Id = await _worksheetController.CreateAsync(worksheet);
                            foreach(var column in worksheet.Columns)
                            {
                                column.WorksheetId = worksheet.Id;
                                column.Id = await _columnController.CreateAsync(column);
                            }
                            foreach (var row in worksheet.Rows)
                            {
                                row.WorksheetId = worksheet.Id;
                                row.Id = await _rowController.CreateAsync(row);
                                foreach (var cell in  row.Cells)
                                {
                                    cell.ColumnId = worksheet.Columns.First(x => x.Position == cell.Position).Id;
                                    cell.RowId = row.Id;
                                    cell.Id = await _cellController.CreateAsync(cell);
                                }
                            }
                        }
                    }
                    _logger.LogInformation("Data added to database");

                    _logger.LogInformation("Retrieving data from database");
                    var dbWorkbooks = await _workbookController.GetAsync();
                    foreach (var dbWorkbook in dbWorkbooks)
                    {
                        dbWorkbook.Worksheets.AddRange(await _worksheetController.GetByWorkbookIdAsync(dbWorkbook.Id));
                        
                        foreach (var dbWorksheet in dbWorkbook.Worksheets)
                        {
                            dbWorksheet.Columns.AddRange(await _columnController.GetByWorksheetIdAsync(dbWorksheet.Id));
                            dbWorksheet.Rows.AddRange(await _rowController.GetByWorksheetIdAsync(dbWorksheet.Id));

                            foreach (var dbRow in dbWorksheet.Rows)
                            {
                                dbRow.Cells.AddRange(await _cellController.GetByRowIdAsync(dbRow.Id));
                            }
                        }
                    }
                    _logger.LogInformation("Data retrieved from database");

                    _logger.LogInformation("Generating tables");
                    foreach (var workbook in dbWorkbooks)
                    {
                        foreach(var worksheet in workbook.Worksheets)
                        {
                            var table = TableEngine.GetTable(worksheet);
                            AnsiConsole.Write(table);
                        }
                    }
                    _logger.LogInformation("Tables generated");

                    _logger.LogInformation("Press any key to continue...");
                    Console.ReadKey();
                    _exitCode = 0;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Exception Message = {ExceptionMessage}", exception.Message);
                    _exitCode = 1;
                }
                finally
                {
                    _appLifetime.StopApplication();
                }
            });
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Environment.ExitCode = _exitCode.GetValueOrDefault(-1);
        return Task.CompletedTask;
    }

    #endregion
}
