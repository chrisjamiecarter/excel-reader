using ExcelReader.Models;

namespace ExcelReader.ConsoleApp.Controllers;
public interface ICellController
{
    Task<int> CreateAsync(Cell cell);
    Task<bool> DeleteAsync(Cell cell);
    Task<IReadOnlyList<Cell>> GetAsync();
    Task<Cell?> GetAsync(int id);
    Task<IReadOnlyList<Cell>> GetByRowIdAsync(int rowId);
    Task<bool> UpdateAsync(Cell cell);
}