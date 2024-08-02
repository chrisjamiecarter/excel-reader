using ExcelReader.Models;
using Spectre.Console;

namespace ExcelReader.ConsoleApp.Engines;

/// <summary>
/// Engine for Spectre.Table generation.
/// </summary>
internal class TableEngine
{
    #region Methods

    internal static Table GetTable(Worksheet worksheet)
    {
        var table = new Table
        {
            Caption = new TableTitle($"{worksheet.Rows.Count} rows."),
            Expand = true,
        };

        foreach(var column in worksheet.Columns.OrderBy(o => o.Position))
        {
            table.AddColumn(column.Name);
        }

        foreach(var row in worksheet.Rows.OrderBy(o => o.Position))
        {
            table.AddRow(row.Cells.OrderBy(c => c.Position).Select(c => c.Value).ToArray());
        }
        
        return table;
    }

    #endregion
}
