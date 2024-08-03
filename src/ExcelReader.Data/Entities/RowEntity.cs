using ExcelReader.Models;
using SQLite;
using static Dapper.SqlMapper;

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

    public static RowEntity MapFrom(DataRow row)
    {
        ArgumentNullException.ThrowIfNull(row, nameof(row));
        
        return new RowEntity
        {
            Id = row.Id,
            WorksheetId = row.DataSheetId,
            Position = row.Position,
        };
    }

    public static DataRow MapTo(RowEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        
        return new DataRow
        {
            Id = entity.Id,
            DataSheetId = entity.WorksheetId,
            Position = entity.Position,
        };
    }

    #endregion
}
