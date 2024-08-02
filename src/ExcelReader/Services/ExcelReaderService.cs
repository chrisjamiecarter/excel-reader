using System.Data;
using ExcelReader.Models;
using OfficeOpenXml;

namespace ExcelReader.Services;

public class ExcelReaderService
{
    public List<Workbook> Process(string directoryPath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(directoryPath, nameof(directoryPath));

        List<Workbook> workbooks = [];

        foreach(var fileInfo in new DirectoryInfo(directoryPath).EnumerateFiles())
        {
            if (Path.GetExtension(fileInfo.Extension) == ".xlsx")
            {
                var workbook = new Workbook
                {
                    DirectoryPath = fileInfo.DirectoryName!,
                    Name = Path.GetFileNameWithoutExtension(fileInfo.Name),
                    Extension = fileInfo.Extension,
                    Size = fileInfo.Length
                };

                using var package = new ExcelPackage(fileInfo);

                int worksheetIndex = 0;
                foreach (var sheet in package.Workbook.Worksheets)
                {
                    var worksheet = new Worksheet
                    {
                        Name = sheet.Name,
                        Position = worksheetIndex++,
                    };

                    // Columns.
                    for (int x = 0; x < sheet.Dimension.End.Column; x++)
                    {
                        var column = new Column
                        {
                            Position = x,
                            Name = sheet.Cells[1, x + 1].Text,
                        };
                        worksheet.Columns.Add(column);
                    }

                    // Rows and Cells.
                    for (int y = 1; y < sheet.Dimension.End.Row; y++)
                    {
                        var row = new Row
                        {
                            Position = y - 1
                        };
                        for (int z = 0; z < sheet.Dimension.End.Column; z++)
                        {
                            var cell = new Cell
                            {
                                Position = z,
                                Value = sheet.Cells[y + 1, z + 1].Text
                            };
                            row.Cells.Add(cell);
                        }
                        worksheet.Rows.Add(row);
                    }
                    
                    workbook.Worksheets.Add(worksheet);
                }

                workbooks.Add(workbook);
            }
            else
            {
                Console.WriteLine("NOT EXCEL");
            }

        }

        return workbooks;
    }
}
