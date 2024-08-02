using ExcelReader.Models;
using SQLite;

namespace ExcelReader.Data.Entities;

[Table("Worksheet")]
public class WorksheetEntity
{
    #region Properties

    [PrimaryKey]
    [NotNull]
    [AutoIncrement]
    public int Id { get; set; }

    [NotNull]
    public int WorkbookId { get; set; }

    [NotNull]
    public int Position { get; set; }

    [NotNull]
    public string Name { get; set; } = string.Empty;

    #endregion
    #region Methods

    public static WorksheetEntity MapFrom(Worksheet worksheet)
    {
        ArgumentNullException.ThrowIfNull(worksheet, nameof(worksheet));
        ArgumentException.ThrowIfNullOrWhiteSpace(worksheet.Name, nameof(worksheet.Name));
        
        return new WorksheetEntity
        {
            Id = worksheet.Id,
            Position = worksheet.Position,
            Name = worksheet.Name,
        };
    }

    public static Worksheet MapTo(WorksheetEntity worksheet)
    {
        ArgumentNullException.ThrowIfNull(worksheet, nameof(worksheet));
        ArgumentException.ThrowIfNullOrWhiteSpace(worksheet.Name, nameof(worksheet.Name));
        
        return new Worksheet
        {
            Id = worksheet.Id,
            Position = worksheet.Position,
            Name = worksheet.Name,
        };
    }

    #endregion
}
