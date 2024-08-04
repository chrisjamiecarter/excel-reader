using ExcelReader.Models;
using OfficeOpenXml;

namespace ExcelReader.Services;

public class ExcelDataFileReader : DataFileReader, IExcelDataFileReader
{
    public DataFile ReadDataFile(FileInfo fileInfo)
    {
        ArgumentNullException.ThrowIfNull(fileInfo, nameof(fileInfo));

        var dataFile = new DataFile
        {
            Name = Path.GetFileNameWithoutExtension(fileInfo.Name),
            Extension = fileInfo.Extension.ToLower(),
            Size = fileInfo.Length
        };

        using var package = new ExcelPackage(fileInfo);

        dataFile.DataSheets = GenerateDataSheets(package);

        return dataFile;
    }
}
