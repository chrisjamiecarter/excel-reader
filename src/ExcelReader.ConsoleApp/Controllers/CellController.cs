using ExcelReader.Data.Entities;
using ExcelReader.Data.Repositories;
using ExcelReader.Models;

namespace ExcelReader.ConsoleApp.Controllers;

public class CellController : ICellController
{
    private readonly IUnitOfWork _unitOfWork;

    public CellController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Creates the Cell in the Repository and returns the ID of the created entity.
    /// </summary>
    /// <param name="cell">The Cell to create in the Repository.</param>
    /// <returns>The ID of the created entity.</returns>
    public async Task<int> CreateAsync(Cell cell)
    {
        var entity = CellEntity.MapFrom(cell);

        return await _unitOfWork.Cells.AddAndGetIdAsync(entity);
    }

    public async Task<IReadOnlyList<Cell>> GetAsync()
    {
        var output = await _unitOfWork.Cells.GetAsync();
        return output.Select(CellEntity.MapTo).ToList();
    }

    public async Task<Cell?> GetAsync(int id)
    {
        var output = await _unitOfWork.Cells.GetAsync(id);
        return output is null ? null : CellEntity.MapTo(output);
    }

    public async Task<bool> UpdateAsync(Cell cell)
    {
        var entity = CellEntity.MapFrom(cell);

        var result = await _unitOfWork.Cells.UpdateAsync(entity);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Cell cell)
    {
        var entity = CellEntity.MapFrom(cell);

        var result = await _unitOfWork.Cells.DeleteAsync(entity);

        return result > 0;
    }
}
