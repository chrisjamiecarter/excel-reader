using ExcelReader.Models;

namespace ExcelReader.ConsoleApp.Controllers;
public interface IRowController
{
    Task<int> CreateAsync(Row row);
    Task<bool> DeleteAsync(Row row);
    Task<IReadOnlyList<Row>> GetAsync();
    Task<Row?> GetAsync(int id);
    Task<IReadOnlyList<Row>> GetByWorksheetIdAsync(int worksheetId);
    Task<bool> UpdateAsync(Row row);
}