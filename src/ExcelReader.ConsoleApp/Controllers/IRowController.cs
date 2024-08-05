using ExcelReader.Models;

namespace ExcelReader.ConsoleApp.Controllers;
public interface IRowController
{
    Task<int> CreateAsync(DataSheetRow row);
    Task<bool> DeleteAsync(DataSheetRow row);
    Task<IReadOnlyList<DataSheetRow>> GetAsync();
    Task<DataSheetRow?> GetAsync(int id);
    Task<IReadOnlyList<DataSheetRow>> GetByWorksheetIdAsync(int worksheetId);
    Task<bool> UpdateAsync(DataSheetRow row);
}