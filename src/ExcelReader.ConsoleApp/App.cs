using ExcelReader.Configurations;
using ExcelReader.ConsoleApp.Controllers;
using ExcelReader.ConsoleApp.Engines;
using ExcelReader.Constants;
using ExcelReader.Models;
using ExcelReader.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
    private readonly ApplicationOptions _options;
    private readonly IDatabaseController _databaseController;
    private readonly IWorkbookController _workbookController;
    private readonly IWorksheetController _worksheetController;
    private readonly IColumnController _columnController;
    private readonly IRowController _rowController;
    private readonly ICellController _cellController;
    private readonly IDataFileProcessor _dataFileProcessor;
    private int? _exitCode;

    #endregion
    #region Constructors

    public App(
        IHostApplicationLifetime appLifetime,
        ILogger<App> logger,
        IOptions<ApplicationOptions> options,
        IDatabaseController databaseController,
        IWorkbookController workbookController,
        IWorksheetController worksheetController,
        IColumnController columnController,
        IRowController rowController,
        ICellController cellController,
        IDataFileProcessor dataFileProcessor)
    {
        _appLifetime = appLifetime;
        _logger = logger;
        _options = options.Value;
        _databaseController = databaseController;
        _workbookController = workbookController;
        _worksheetController = worksheetController;
        _columnController = columnController;
        _rowController = rowController;
        _cellController = cellController;
        _dataFileProcessor = dataFileProcessor;
    }

    #endregion
    #region Properties

    private string DoneDirectoryPath => Path.GetFullPath(Path.Combine(_options.WorkingDirectoryPath, DirectoryName.Done));

    private string ErrorDirectoryPath => Path.GetFullPath(Path.Combine(_options.WorkingDirectoryPath, DirectoryName.Error));

    private string IncomingDirectoryPath => Path.GetFullPath(Path.Combine(_options.WorkingDirectoryPath, DirectoryName.Incoming));
    
    private string ProcessingDirectoryPath => Path.GetFullPath(Path.Combine(_options.WorkingDirectoryPath, DirectoryName.Processing));
    
    #endregion
    #region Methods - Public

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _appLifetime.ApplicationStarted.Register(() =>
        {
            Task.Run(async () =>
            {
                try
                {
                    _logger.LogInformation("Application started");

                    // Configure working directory.
                    ConfigureWorkingDirectory();

                    // Configure database.
                    _logger.LogInformation("Resetting database");
                    _databaseController.Reset();
                    _logger.LogInformation("Database reset");

                    // Process incoming files.
                    List<DataFile> incomingDataFiles = [];
                    _logger.LogInformation("Processing incoming directory {directory}", IncomingDirectoryPath);
                    foreach (var fileInfo in new DirectoryInfo(IncomingDirectoryPath).EnumerateFiles())
                    {
                        _logger.LogInformation("Processing file {file}", fileInfo.Name);

                        fileInfo.MoveTo(Path.Combine(ProcessingDirectoryPath, fileInfo.Name), true);

                        try
                        {
                            incomingDataFiles.Add(_dataFileProcessor.ProcessFile(fileInfo));

                            fileInfo.MoveTo(Path.Combine(DoneDirectoryPath, fileInfo.Name), true);

                            _logger.LogInformation("File processed");
                        }
                        catch (Exception exception)
                        {
                            _logger.LogWarning("Error procesing file {message}", exception.Message);

                            fileInfo.MoveTo(Path.Combine(ErrorDirectoryPath, fileInfo.Name), true);

                            _logger.LogInformation("File aborted");
                        }
                    }
                    _logger.LogInformation("Incoming directory processed");


                    // Process database - ADD.

                    _logger.LogInformation("Adding data to database");
                    foreach (var workbook in incomingDataFiles)
                    {
                        workbook.Id = await _workbookController.CreateAsync(workbook);
                        foreach (var worksheet in workbook.DataSheets)
                        {
                            worksheet.DataFileId = workbook.Id;
                            worksheet.Id = await _worksheetController.CreateAsync(worksheet);
                            foreach (var column in worksheet.DataFields)
                            {
                                column.DataSheetId = worksheet.Id;
                                column.Id = await _columnController.CreateAsync(column);
                            }
                            foreach (var row in worksheet.DataRows)
                            {
                                row.DataSheetId = worksheet.Id;
                                row.Id = await _rowController.CreateAsync(row);
                                foreach (var cell in row.DataItems)
                                {
                                    cell.DataFieldId = worksheet.DataFields.First(x => x.Position == cell.Position).Id;
                                    cell.DataRowId = row.Id;
                                    cell.Id = await _cellController.CreateAsync(cell);
                                }
                            }
                        }
                    }
                    _logger.LogInformation("Data added to database");

                    // Process database - GET.

                    _logger.LogInformation("Retrieving data from database");
                    var dbWorkbooks = await _workbookController.GetAsync();
                    foreach (var dbWorkbook in dbWorkbooks)
                    {
                        dbWorkbook.DataSheets.AddRange(await _worksheetController.GetByWorkbookIdAsync(dbWorkbook.Id));

                        foreach (var dbWorksheet in dbWorkbook.DataSheets)
                        {
                            dbWorksheet.DataFields.AddRange(await _columnController.GetByWorksheetIdAsync(dbWorksheet.Id));
                            dbWorksheet.DataRows.AddRange(await _rowController.GetByWorksheetIdAsync(dbWorksheet.Id));

                            foreach (var dbRow in dbWorksheet.DataRows)
                            {
                                dbRow.DataItems.AddRange(await _cellController.GetByRowIdAsync(dbRow.Id));
                            }
                        }
                    }
                    _logger.LogInformation("Data retrieved from database");

                    // Process reports.

                    _logger.LogInformation("Generating tables");
                    foreach (var workbook in dbWorkbooks)
                    {
                        foreach (var worksheet in workbook.DataSheets)
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
    #region Methods - Private

    private void ConfigureWorkingDirectory()
    {
        _logger.LogInformation("Starting {method}", nameof(ConfigureWorkingDirectory));

        if (!Directory.Exists(DoneDirectoryPath))
        {
            _logger.LogInformation("Creating {directory}", DoneDirectoryPath);
            Directory.CreateDirectory(DoneDirectoryPath);
        }
        if (!Directory.Exists(ErrorDirectoryPath))
        {
            _logger.LogInformation("Creating {directory}", ErrorDirectoryPath);
            Directory.CreateDirectory(ErrorDirectoryPath);
        }
        if (!Directory.Exists(IncomingDirectoryPath))
        {
            _logger.LogInformation("Creating {directory}", IncomingDirectoryPath);
            Directory.CreateDirectory(IncomingDirectoryPath);
        }
        if (!Directory.Exists(ProcessingDirectoryPath))
        {
            _logger.LogInformation("Creating {directory}", ProcessingDirectoryPath);
            Directory.CreateDirectory(ProcessingDirectoryPath);
        }

        _logger.LogInformation("Finished {method}", nameof(ConfigureWorkingDirectory));
    }

#endregion

}
