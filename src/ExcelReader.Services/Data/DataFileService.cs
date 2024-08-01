using ExcelReader.Data.Entities;
using ExcelReader.Data.Repositories;
using ExcelReader.Models;

namespace ExcelReader.Services.Data;

public class DataFileService
{
    private readonly IDataFileRepository _dataFileRepository;

    public DataFileService(IDataFileRepository dataFileRepository)
    {
        _dataFileRepository = dataFileRepository;
    }

    public async Task<bool> CreateAsync(DataFile dataFile)
    {
        var entity = DataFileEntity.MapFrom(dataFile);

        var result = await _dataFileRepository.AddAsync(entity);

        return result > 0;
    }

    public async Task<IReadOnlyList<DataFile>> GetAsync()
    {
        var output = await _dataFileRepository.GetAsync();
        return output.Select(DataFileEntity.MapTo).ToList();
    }

    public async Task<DataFile?> GetAsync(int id)
    {
        var output = await _dataFileRepository.GetAsync(id);
        return output is null ? null : DataFileEntity.MapTo(output);
    }

    public async Task<bool> UpdateAsync(DataFile dataFile)
    {
        var entity = DataFileEntity.MapFrom(dataFile);

        var result = await _dataFileRepository.UpdateAsync(entity);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(DataFile dataFile)
    {
        var entity = DataFileEntity.MapFrom(dataFile);

        var result = await _dataFileRepository.DeleteAsync(entity);

        return result > 0;
    }
}
