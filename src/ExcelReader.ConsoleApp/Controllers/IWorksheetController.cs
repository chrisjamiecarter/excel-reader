using ExcelReader.Models;

namespace ExcelReader.ConsoleApp.Controllers;
public interface IWorksheetController
{
    Task<int> CreateAsync(DataSheet worksheet);
    Task<bool> DeleteAsync(DataSheet worksheet);
    Task<IReadOnlyList<DataSheet>> GetAsync();
    Task<DataSheet?> GetAsync(int id);
    Task<IReadOnlyList<DataSheet>> GetByWorkbookIdAsync(int workbookId);
    Task<bool> UpdateAsync(DataSheet worksheet);
}