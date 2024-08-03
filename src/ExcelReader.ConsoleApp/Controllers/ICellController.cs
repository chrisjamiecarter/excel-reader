using ExcelReader.Models;

namespace ExcelReader.ConsoleApp.Controllers;
public interface ICellController
{
    Task<int> CreateAsync(DataItem cell);
    Task<bool> DeleteAsync(DataItem cell);
    Task<IReadOnlyList<DataItem>> GetAsync();
    Task<DataItem?> GetAsync(int id);
    Task<IReadOnlyList<DataItem>> GetByRowIdAsync(int rowId);
    Task<bool> UpdateAsync(DataItem cell);
}