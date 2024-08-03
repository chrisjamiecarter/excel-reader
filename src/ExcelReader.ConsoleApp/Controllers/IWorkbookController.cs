using ExcelReader.Models;

namespace ExcelReader.ConsoleApp.Controllers;

public interface IWorkbookController
{
    Task<int> CreateAsync(DataFile workbook);
    Task<bool> DeleteAsync(DataFile workbook);
    Task<IReadOnlyList<DataFile>> GetAsync();
    Task<DataFile?> GetAsync(int id);
    Task<bool> UpdateAsync(DataFile workbook);
}