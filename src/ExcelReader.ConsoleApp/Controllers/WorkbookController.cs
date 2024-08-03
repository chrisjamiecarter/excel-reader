using ExcelReader.Data.Entities;
using ExcelReader.Data.Repositories;
using ExcelReader.Models;

namespace ExcelReader.ConsoleApp.Controllers;

public class WorkbookController : IWorkbookController
{
    private readonly IUnitOfWork _unitOfWork;

    public WorkbookController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

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

    public async Task<IReadOnlyList<DataFile>> GetAsync()
    {
        var output = await _unitOfWork.Workbooks.GetAsync();
        return output.Select(WorkbookEntity.MapTo).ToList();
    }

    public async Task<DataFile?> GetAsync(int id)
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
