using ExcelReader.Data.Entities;
using ExcelReader.Data.Repositories;
using ExcelReader.Models;

namespace ExcelReader.Services;

public partial class DatabaseService : IDatabaseService
{
    /// <summary>
    /// Creates the Row in the Repository and returns the ID of the created entity.
    /// </summary>
    /// <param name="row">The Row to create in the Repository.</param>
    /// <returns>The ID of the created entity.</returns>
    public async Task<int> CreateAsync(DataSheetRow row)
    {
        var entity = RowEntity.MapFrom(row);

        return await _unitOfWork.Rows.AddAndGetIdAsync(entity);
    }

    public async Task<IReadOnlyList<DataSheetRow>> GetDataSheetRowsAsync()
    {
        var output = await _unitOfWork.Rows.GetAsync();
        return output.Select(RowEntity.MapTo).ToList();
    }

    public async Task<DataSheetRow?> GetDataSheetRowAsync(int id)
    {
        var output = await _unitOfWork.Rows.GetAsync(id);
        return output is null ? null : RowEntity.MapTo(output);
    }

    public async Task<IReadOnlyList<DataSheetRow>> GetDataSheetRowsByWorksheetIdAsync(int worksheetId)
    {
        var output = await _unitOfWork.Rows.GetByWorksheetIdAsync(worksheetId);
        return output.Select(RowEntity.MapTo).ToList();
    }
    public async Task<bool> UpdateAsync(DataSheetRow row)
    {
        var entity = RowEntity.MapFrom(row);

        var result = await _unitOfWork.Rows.UpdateAsync(entity);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(DataSheetRow row)
    {
        var entity = RowEntity.MapFrom(row);

        var result = await _unitOfWork.Rows.DeleteAsync(entity);

        return result > 0;
    }
}
