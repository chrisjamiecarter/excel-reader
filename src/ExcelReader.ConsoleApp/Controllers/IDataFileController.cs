using ExcelReader.Models;

namespace ExcelReader.ConsoleApp.Controllers;
public interface IDataFileController
{
    Task<bool> CreateAsync(DataFile dataFile);
    Task<bool> DeleteAsync(DataFile dataFile);
    Task<IReadOnlyList<DataFile>> GetAsync();
    Task<DataFile?> GetAsync(int id);
    Task<bool> UpdateAsync(DataFile dataFile);
}