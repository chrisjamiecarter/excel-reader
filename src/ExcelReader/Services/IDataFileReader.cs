using ExcelReader.Models;
using OfficeOpenXml;

namespace ExcelReader.Services;
public interface IDataFileReader
{
    List<DataField> GenerateDataFields(ExcelWorksheet worksheet);
    List<DataRow> GenerateDataRows(ExcelWorksheet worksheet);
    List<DataSheet> GenerateDataSheets(ExcelPackage package);
}