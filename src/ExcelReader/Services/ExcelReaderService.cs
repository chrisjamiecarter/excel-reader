using System.Text;
using ExcelReader.Enums;
using ExcelReader.Models;
using OfficeOpenXml;

namespace ExcelReader.Services;

public class ExcelReaderService
{
    public static Workbook GenerateWorkbookFromFile(FileInfo fileInfo)
    {
        ArgumentNullException.ThrowIfNull(fileInfo, nameof(fileInfo));

        var workbook = new Workbook
        {
            Name = Path.GetFileNameWithoutExtension(fileInfo.Name),
            Extension = fileInfo.Extension.ToLower(),
            Size = fileInfo.Length
        };

        var support = GetSupportedFileExtension(workbook.Extension);

        using var package = GetExcelPackageForSupportedFileExtension(support, fileInfo);

        workbook.Worksheets.AddRange(GenerateWorksheets(package));

        return workbook;
    }

    private static List<Worksheet> GenerateWorksheets(ExcelPackage package)
    {
        List<Worksheet> worksheets = [];

        int worksheetIndex = 0;
        foreach (var excelWorksheet in package.Workbook.Worksheets)
        {
            var worksheet = new Worksheet
            {
                Name = excelWorksheet.Name,
                Position = worksheetIndex++,
            };

            // Columns.
            worksheet.Columns.AddRange(GenerateColumns(excelWorksheet));
            
            // Rows and Cells.
            worksheet.Rows.AddRange(GenerateRows(excelWorksheet));
                        
            worksheets.Add(worksheet);
        }

        return worksheets;
    }

    private static List<Column> GenerateColumns(ExcelWorksheet excelWorksheet)
    {
        List<Column> columns = [];

        for (int x = 0; x < excelWorksheet.Dimension.End.Column; x++)
        {
            var column = new Column
            {
                Position = x,
                Name = excelWorksheet.Cells[1, x + 1].Text,
            };

            columns.Add(column);
        }

        return columns;
    }

    private static List<Row> GenerateRows(ExcelWorksheet excelWorksheet)
    {
        List<Row> rows = [];

        for (int y = 1; y < excelWorksheet.Dimension.End.Row; y++)
        {
            var row = new Row
            {
                Position = y - 1
            };

            for (int z = 0; z < excelWorksheet.Dimension.End.Column; z++)
            {
                var cell = new Cell
                {
                    Position = z,
                    Value = excelWorksheet.Cells[y + 1, z + 1].Text
                };

                row.Cells.Add(cell);
            }

            if (IsValidRow(row))
            {
                rows.Add(row);
            }
        }

        return rows;
    }

    private static bool IsValidRow(Row row)
    {
        return row.Cells.All(x => !string.IsNullOrEmpty(x.Value));
    }

    private static SupportedFileExtension GetSupportedFileExtension(string extension)
    {
        return extension switch
        {
            ".csv" => SupportedFileExtension.CSV,
            ".xlsx" => SupportedFileExtension.XLSX,
            _ => throw new ArgumentOutOfRangeException(nameof(extension)),
        };
    }

    private static ExcelPackage GetExcelPackageForSupportedFileExtension(SupportedFileExtension support, FileInfo fileInfo)
    {
        switch (support)
        {
            case SupportedFileExtension.CSV:

                var excelPackage = new ExcelPackage();
                var worksheet = excelPackage.Workbook.Worksheets.Add(fileInfo.Name);
                worksheet.Cells["A1"].LoadFromText(fileInfo, new ExcelTextFormat
                {
                    Delimiter = ',',
                    Encoding = new UTF8Encoding()
                });
                return excelPackage;

            case SupportedFileExtension.XLSX:

                return new ExcelPackage(fileInfo);

            default:

                throw new ArgumentOutOfRangeException(nameof(support));
        }
    }
}
