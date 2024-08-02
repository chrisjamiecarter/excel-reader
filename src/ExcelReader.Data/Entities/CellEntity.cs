using ExcelReader.Models;
using SQLite;

namespace ExcelReader.Data.Entities;

[Table("Cell")]
public class CellEntity
{
    #region Properties

    [PrimaryKey]
    [NotNull]
    [AutoIncrement]
    public int Id { get; set; }

    [NotNull]
    public int ColumnId { get; set; }

    [NotNull]
    public int RowId { get; set; }

    [NotNull]
    public int Position { get; set; }

    [NotNull]
    public string Value { get; set; } = string.Empty;

    #endregion
    #region Methods

    public static CellEntity MapFrom(Cell cell)
    {
        ArgumentNullException.ThrowIfNull(cell, nameof(cell));
        ArgumentNullException.ThrowIfNull(cell.Value, nameof(cell.Value));

        return new CellEntity
        {
            Id = cell.Id,
            Position = cell.Position,
            Value = cell.Value,
        };
    }

    public static Cell MapTo(CellEntity cell)
    {
        ArgumentNullException.ThrowIfNull(cell, nameof(cell));
        ArgumentNullException.ThrowIfNull(cell.Value, nameof(cell.Value));

        return new Cell
        {
            Id = cell.Id,
            ColumnId = cell.ColumnId,
            RowId = cell.RowId,
            Position = cell.Position,
            Value = cell.Value,
        };
    }

    #endregion
}
