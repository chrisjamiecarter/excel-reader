using ExcelReader.Data.Entities;
using ExcelReader.Data.Repositories;
using ExcelReader.Models;

namespace ExcelReader.ConsoleApp.Controllers;

public class WorksheetController : IWorksheetController
{
    private readonly IUnitOfWork _unitOfWork;

    public WorksheetController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Creates the Worksheet in the Repository and returns the ID of the created entity.
    /// </summary>
    /// <param name="worksheet">The Worksheet to create in the Repository.</param>
    /// <returns>The ID of the created entity.</returns>
    public async Task<int> CreateAsync(Worksheet worksheet)
    {
        var entity = WorksheetEntity.MapFrom(worksheet);

        return await _unitOfWork.Worksheets.AddAndGetIdAsync(entity);
    }

    public async Task<IReadOnlyList<Worksheet>> GetAsync()
    {
        var output = await _unitOfWork.Worksheets.GetAsync();
        return output.Select(WorksheetEntity.MapTo).ToList();
    }

    public async Task<Worksheet?> GetAsync(int id)
    {
        var output = await _unitOfWork.Worksheets.GetAsync(id);
        return output is null ? null : WorksheetEntity.MapTo(output);
    }

    public async Task<bool> UpdateAsync(Worksheet worksheet)
    {
        var entity = WorksheetEntity.MapFrom(worksheet);

        var result = await _unitOfWork.Worksheets.UpdateAsync(entity);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Worksheet worksheet)
    {
        var entity = WorksheetEntity.MapFrom(worksheet);

        var result = await _unitOfWork.Worksheets.DeleteAsync(entity);

        return result > 0;
    }
}
