using ExcelReader.Data.Entities;
using ExcelReader.Data.Repositories;
using ExcelReader.Models;

namespace ExcelReader.ConsoleApp.Controllers;

public class ColumnController : IColumnController
{
    private readonly IUnitOfWork _unitOfWork;

    public ColumnController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Creates the Column in the Repository and returns the ID of the created entity.
    /// </summary>
    /// <param name="column">The Column to create in the Repository.</param>
    /// <returns>The ID of the created entity.</returns>
    public async Task<int> CreateAsync(Column column)
    {
        var entity = ColumnEntity.MapFrom(column);

        return await _unitOfWork.Columns.AddAndGetIdAsync(entity);
    }

    public async Task<IReadOnlyList<Column>> GetAsync()
    {
        var output = await _unitOfWork.Columns.GetAsync();
        return output.Select(ColumnEntity.MapTo).ToList();
    }

    public async Task<Column?> GetAsync(int id)
    {
        var output = await _unitOfWork.Columns.GetAsync(id);
        return output is null ? null : ColumnEntity.MapTo(output);
    }

    public async Task<IReadOnlyList<Column>> GetByWorksheetIdAsync(int worksheetId)
    {
        var output = await _unitOfWork.Columns.GetByWorksheetIdAsync(worksheetId);
        return output.Select(ColumnEntity.MapTo).ToList();
    }

    public async Task<bool> UpdateAsync(Column column)
    {
        var entity = ColumnEntity.MapFrom(column);

        var result = await _unitOfWork.Columns.UpdateAsync(entity);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Column column)
    {
        var entity = ColumnEntity.MapFrom(column);

        var result = await _unitOfWork.Columns.DeleteAsync(entity);

        return result > 0;
    }
}
