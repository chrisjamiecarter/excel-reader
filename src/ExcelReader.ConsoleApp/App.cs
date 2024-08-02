using ExcelReader.ConsoleApp.Controllers;
using ExcelReader.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
                    _databaseController.Reset();

                    var inputFilePath = Path.GetFullPath("..\\..\\..\\..\\..\\_test\\");
                    var reader = new ExcelReaderService();
                    var workbooks = reader.Process(inputFilePath);

                    foreach (var workbook in workbooks)
                    {
                        workbook.Id = await _workbookController.CreateAsync(workbook);
                        _logger.LogInformation("Inserted Workbook: {workbook}", workbook);

                        foreach (var worksheet in workbook.Worksheets)
                        {
                            worksheet.WorkbookId = workbook.Id;
                            worksheet.Id = await _worksheetController.CreateAsync(worksheet);
                            _logger.LogInformation("Inserted Worksheet: {worksheet}", worksheet);

                            foreach(var column in worksheet.Columns)
                            {
                                column.WorksheetId = worksheet.Id;
                                column.Id = await _columnController.CreateAsync(column);
                                _logger.LogInformation("Inserted Column: {column}", column);
                            }

                            foreach (var row in worksheet.Rows)
                            {
                                row.WorksheetId = worksheet.Id;
                                row.Id = await _rowController.CreateAsync(row);
                                _logger.LogInformation("Inserted Row: {row}", row);

                                foreach (var cell in  row.Cells)
                                {
                                    cell.ColumnId = worksheet.Columns.First(x => x.Position == cell.Position).Id;
                                    cell.RowId = row.Id;
                                    cell.Id = await _cellController.CreateAsync(cell);
                                    _logger.LogInformation("Inserted Cell: {cell}", cell);
                                }
                            }
                        }
                    }

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
