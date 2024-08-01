using ExcelReader.Data.Entities;
using ExcelReader.Data.Repositories;
using ExcelReader.Models;

namespace ExcelReader.ConsoleApp.Controllers;

public class DataFileController : IDataFileController
{
    private readonly IUnitOfWork _unitOfWork;

    public DataFileController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> CreateAsync(DataFile dataFile)
    {
        var entity = DataFileEntity.MapFrom(dataFile);

        var result = await _unitOfWork.DataFiles.AddAsync(entity);

        return result > 0;
    }

    public async Task<IReadOnlyList<DataFile>> GetAsync()
    {
        var output = await _unitOfWork.DataFiles.GetAsync();
        return output.Select(DataFileEntity.MapTo).ToList();
    }

    public async Task<DataFile?> GetAsync(int id)
    {
        var output = await _unitOfWork.DataFiles.GetAsync(id);
        return output is null ? null : DataFileEntity.MapTo(output);
    }

    public async Task<bool> UpdateAsync(DataFile dataFile)
    {
        var entity = DataFileEntity.MapFrom(dataFile);

        var result = await _unitOfWork.DataFiles.UpdateAsync(entity);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(DataFile dataFile)
    {
        var entity = DataFileEntity.MapFrom(dataFile);

        var result = await _unitOfWork.DataFiles.DeleteAsync(entity);

        return result > 0;
    }
}
