using ExcelReader.Models;

namespace ExcelReader.ConsoleApp.Controllers;
public interface IWorksheetController
{
    Task<int> CreateAsync(Worksheet worksheet);
    Task<bool> DeleteAsync(Worksheet worksheet);
    Task<IReadOnlyList<Worksheet>> GetAsync();
    Task<Worksheet?> GetAsync(int id);
    Task<bool> UpdateAsync(Worksheet worksheet);
}