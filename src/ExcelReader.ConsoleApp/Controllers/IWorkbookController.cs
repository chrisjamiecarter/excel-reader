using ExcelReader.Models;

namespace ExcelReader.ConsoleApp.Controllers;

public interface IWorkbookController
{
    Task<int> CreateAsync(Workbook workbook);
    Task<bool> DeleteAsync(Workbook workbook);
    Task<IReadOnlyList<Workbook>> GetAsync();
    Task<Workbook?> GetAsync(int id);
    Task<bool> UpdateAsync(Workbook workbook);
}