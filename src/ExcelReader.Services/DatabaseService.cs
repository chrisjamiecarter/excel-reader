using ExcelReader.Data.Entities;
using ExcelReader.Data.Repositories;
using ExcelReader.Models;
using Microsoft.Extensions.Logging;

namespace ExcelReader.Services;

public partial class DatabaseService : IDatabaseService
{
    private readonly ILogger<DatabaseService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public DatabaseService(ILogger<DatabaseService> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public bool ResetDatabase()
    {
        _logger.LogInformation("Starting {method}", nameof(ResetDatabase));
        _unitOfWork.Database.EnsureDeleted();
        _unitOfWork.Database.EnsureCreated();
        _logger.LogInformation("Finished {method}", nameof(ResetDatabase));

        return true;
    }
}
