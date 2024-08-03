using ExcelReader.Models;

namespace ExcelReader.Services;
public interface ICsvDataFileReader
{
    DataFile ReadDataFile(FileInfo fileInfo);
}