using ExcelReader.Models;
using SQLite;
using static Dapper.SqlMapper;

namespace ExcelReader.Data.Entities;

[Table("Column")]
public class ColumnEntity
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

    [NotNull]
    public string Name { get; set; } = string.Empty;

    #endregion
    #region Methods

    public static ColumnEntity MapFrom(DataField column)
    {
        ArgumentNullException.ThrowIfNull(column, nameof(column));
        ArgumentNullException.ThrowIfNull(column.Name, nameof(column.Name));

        return new ColumnEntity
        {
            Id = column.Id,
            WorksheetId = column.DataSheetId,
            Position = column.Position,
            Name = column.Name,
        };
    }

    public static DataField MapTo(ColumnEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        ArgumentNullException.ThrowIfNull(entity.Name, nameof(entity.Name));

        return new DataField
        {
            Id = entity.Id,
            DataSheetId = entity.WorksheetId,
            Position = entity.Position,
            Name = entity.Name,
        };
    }

    #endregion
}
