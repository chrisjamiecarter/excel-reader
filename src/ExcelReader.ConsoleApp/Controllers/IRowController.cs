using ExcelReader.Models;

namespace ExcelReader.ConsoleApp.Controllers;
public interface IRowController
{
    Task<int> CreateAsync(DataRow row);
    Task<bool> DeleteAsync(DataRow row);
    Task<IReadOnlyList<DataRow>> GetAsync();
    Task<DataRow?> GetAsync(int id);
    Task<IReadOnlyList<DataRow>> GetByWorksheetIdAsync(int worksheetId);
    Task<bool> UpdateAsync(DataRow row);
}