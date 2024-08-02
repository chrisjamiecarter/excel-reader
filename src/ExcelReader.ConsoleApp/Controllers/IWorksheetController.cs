using ExcelReader.Models;

namespace ExcelReader.ConsoleApp.Controllers;
public interface IWorksheetController
{
    Task<int> CreateAsync(Worksheet worksheet);
    Task<bool> DeleteAsync(Worksheet worksheet);
    Task<IReadOnlyList<Worksheet>> GetAsync();
    Task<Worksheet?> GetAsync(int id);
    Task<IReadOnlyList<Worksheet>> GetByWorkbookIdAsync(int workbookId);
    Task<bool> UpdateAsync(Worksheet worksheet);
}