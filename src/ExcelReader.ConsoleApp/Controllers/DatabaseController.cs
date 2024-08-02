using ExcelReader.Data.Entities;
using ExcelReader.Data.Repositories;
using ExcelReader.Models;

namespace ExcelReader.ConsoleApp.Controllers;

public class DatabaseController : IDatabaseController
{
    private readonly IUnitOfWork _unitOfWork;

    public DatabaseController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public bool Reset()
    {
        _unitOfWork.Database.EnsureDeleted();
        _unitOfWork.Database.EnsureCreated();
        
        return true;
    }

}
