using ExcelReader.Data.Entities;
using ExcelReader.Data.Repositories;
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
    private readonly IDataFileRepository _dataFileRepository;
    private int? _exitCode;

    #endregion
    #region Constructors

    public App(IHostApplicationLifetime appLifetime, ILogger<App> logger, IDataFileRepository dataFileRepository)
    {
        _appLifetime = appLifetime;
        _logger = logger;
        _dataFileRepository = dataFileRepository;
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
                    var dataFile = new DataFileEntity
                    {
                        Id = 0,
                        DirectoryPath = "TEST_DirectoryPath",
                        Name = "TEST_Name",
                        Extension = ".test",
                        Size = 12345
                    };
                    _logger.LogInformation("Creating = {dataFile}", dataFile);
                    var created = await _dataFileRepository.AddAsync(dataFile);
                    _logger.LogInformation("Result = {isCreated}", created > 0);
                                        
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
