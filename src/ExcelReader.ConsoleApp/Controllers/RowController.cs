using ExcelReader.Data.Entities;
using ExcelReader.Data.Repositories;
using ExcelReader.Models;

namespace ExcelReader.ConsoleApp.Controllers;

public class RowController : IRowController
{
    private readonly IUnitOfWork _unitOfWork;

    public RowController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Creates the Row in the Repository and returns the ID of the created entity.
    /// </summary>
    /// <param name="row">The Row to create in the Repository.</param>
    /// <returns>The ID of the created entity.</returns>
    public async Task<int> CreateAsync(Row row)
    {
        var entity = RowEntity.MapFrom(row);

        return await _unitOfWork.Rows.AddAndGetIdAsync(entity);
    }

    public async Task<IReadOnlyList<Row>> GetAsync()
    {
        var output = await _unitOfWork.Rows.GetAsync();
        return output.Select(RowEntity.MapTo).ToList();
    }

    public async Task<Row?> GetAsync(int id)
    {
        var output = await _unitOfWork.Rows.GetAsync(id);
        return output is null ? null : RowEntity.MapTo(output);
    }

    public async Task<IReadOnlyList<Row>> GetByWorksheetIdAsync(int worksheetId)
    {
        var output = await _unitOfWork.Rows.GetByWorksheetIdAsync(worksheetId);
        return output.Select(RowEntity.MapTo).ToList();
    }
    public async Task<bool> UpdateAsync(Row row)
    {
        var entity = RowEntity.MapFrom(row);

        var result = await _unitOfWork.Rows.UpdateAsync(entity);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Row row)
    {
        var entity = RowEntity.MapFrom(row);

        var result = await _unitOfWork.Rows.DeleteAsync(entity);

        return result > 0;
    }
}
