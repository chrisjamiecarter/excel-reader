using ExcelReader.Models;
using SQLite;

namespace ExcelReader.Data.Entities;

[Table("Row")]
public class RowEntity
{
    #region Properties

    [PrimaryKey]
    [NotNull]
    [AutoIncrement]
    public int Id { get; set; }

    [NotNull]
    public int WorksheetId { get; set; }

    [NotNull]
    public int Position { get; set; }

    #endregion
    #region Methods

    public static RowEntity MapFrom(Row row)
    {
        ArgumentNullException.ThrowIfNull(row, nameof(row));
        
        return new RowEntity
        {
            Id = row.Id,
            Position = row.Position,
        };
    }

    public static Row MapTo(RowEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        
        return new Row
        {
            Id = entity.Id,
            WorksheetId = entity.WorksheetId,
            Position = entity.Position,
        };
    }

    #endregion
}
