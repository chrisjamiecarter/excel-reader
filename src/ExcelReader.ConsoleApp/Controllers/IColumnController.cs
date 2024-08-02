using ExcelReader.Models;

namespace ExcelReader.ConsoleApp.Controllers;
public interface IColumnController
{
    Task<int> CreateAsync(Column column);
    Task<bool> DeleteAsync(Column column);
    Task<IReadOnlyList<Column>> GetAsync();
    Task<Column?> GetAsync(int id);
    Task<IReadOnlyList<Column>> GetByWorksheetIdAsync(int worksheetId);
    Task<bool> UpdateAsync(Column column);
}