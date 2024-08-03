using ExcelReader.Models;

namespace ExcelReader.ConsoleApp.Controllers;
public interface IColumnController
{
    Task<int> CreateAsync(DataField column);
    Task<bool> DeleteAsync(DataField column);
    Task<IReadOnlyList<DataField>> GetAsync();
    Task<DataField?> GetAsync(int id);
    Task<IReadOnlyList<DataField>> GetByWorksheetIdAsync(int worksheetId);
    Task<bool> UpdateAsync(DataField column);
}