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

    public static WorksheetEntity MapFrom(DataSheet worksheet)
    {
        ArgumentNullException.ThrowIfNull(worksheet, nameof(worksheet));
        ArgumentException.ThrowIfNullOrWhiteSpace(worksheet.Name, nameof(worksheet.Name));
        
        return new WorksheetEntity
        {
            Id = worksheet.Id,
            WorkbookId = worksheet.DataFileId,
            Position = worksheet.Position,
            Name = worksheet.Name,
        };
    }

    public static DataSheet MapTo(WorksheetEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        ArgumentException.ThrowIfNullOrWhiteSpace(entity.Name, nameof(entity.Name));
        
        return new DataSheet
        {
            Id = entity.Id,
            DataFileId = entity.WorkbookId,
            Position = entity.Position,
            Name = entity.Name,
        };
    }

    #endregion
}
