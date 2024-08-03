using ExcelReader.Models;

namespace ExcelReader.Services;
public interface IDataFileProcessor
{
    DataFile ProcessFile(FileInfo fileInfo);
}