using ExcelReader.Models;

namespace ExcelReader.Services;
public interface IExcelDataFileReader
{
    DataFile ReadDataFile(FileInfo fileInfo);
}