using ExcelReader.ConsoleApp.Controllers;
using ExcelReader.Models;
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
    private readonly IDataFileController _dataFileController;
    private int? _exitCode;

    #endregion
    #region Constructors

    public App(IHostApplicationLifetime appLifetime, ILogger<App> logger, IDataFileController dataFileController)
    {
        _appLifetime = appLifetime;
        _logger = logger;
        _dataFileController = dataFileController;
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
                    var dataFile1 = new DataFile
                    {
                        Id = 1,
                        DirectoryPath = "TEST_DirectoryPath",
                        Name = "TEST_Name",
                        Extension = ".test",
                        Size = 12345
                    };
                    _logger.LogInformation("Creating = {dataFile}", dataFile1);
                    var created = await _dataFileController.CreateAsync(dataFile1);
                    _logger.LogInformation("Result = {isCreated}", created);

                    _logger.LogInformation("Getting = {dataFile}", dataFile1);
                    var returned = await _dataFileController.GetAsync(dataFile1.Id);
                    _logger.LogInformation("Result = {returned}", returned);

                    _logger.LogInformation("Deleting = {dataFile}", dataFile1);
                    var deleted = await _dataFileController.DeleteAsync(dataFile1);
                    _logger.LogInformation("Result = {isDeleted}", deleted);

                    _logger.LogInformation("Getting = {dataFile}", dataFile1);
                    returned = await _dataFileController.GetAsync(dataFile1.Id);
                    _logger.LogInformation("Result = {returned}", returned);

                    var dataFile2 = new DataFile
                    {
                        Id = 2,
                        DirectoryPath = "TEST_DirectoryPath2",
                        Name = "TEST_Name2",
                        Extension = ".test2",
                        Size = 12345
                    };
                    _logger.LogInformation("Creating = {dataFile}", dataFile2);
                    created = await _dataFileController.CreateAsync(dataFile2);
                    _logger.LogInformation("Result = {isCreated}", created);

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
