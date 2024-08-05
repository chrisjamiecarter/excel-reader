using ExcelReader.Data.Entities;
using ExcelReader.Data.Repositories;
using ExcelReader.Models;

namespace ExcelReader.Services;

public partial class DatabaseService : IDatabaseService
{
    /// <summary>
    /// Creates the Workbook in the Repository and returns the ID of the created entity.
    /// </summary>
    /// <param name="workbook">The Workbook to create in the Repository.</param>
    /// <returns>The ID of the created entity.</returns>
    public async Task<int> CreateAsync(DataFile workbook)
    {
        var entity = WorkbookEntity.MapFrom(workbook);

        return await _unitOfWork.Workbooks.AddAndGetIdAsync(entity);
    }

    public async Task<IReadOnlyList<DataFile>> GetDataFilesAsync()
    {
        var output = await _unitOfWork.Workbooks.GetAsync();
        return output.Select(WorkbookEntity.MapTo).ToList();
    }

    public async Task<DataFile?> GetDataFileAsync(int id)
    {
        var output = await _unitOfWork.Workbooks.GetAsync(id);
        return output is null ? null : WorkbookEntity.MapTo(output);
    }

    public async Task<bool> UpdateAsync(DataFile workbook)
    {
        var entity = WorkbookEntity.MapFrom(workbook);

        var result = await _unitOfWork.Workbooks.UpdateAsync(entity);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(DataFile workbook)
    {
        var entity = WorkbookEntity.MapFrom(workbook);

        var result = await _unitOfWork.Workbooks.DeleteAsync(entity);

        return result > 0;
    }
}
