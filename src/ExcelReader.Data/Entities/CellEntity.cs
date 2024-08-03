﻿using ExcelReader.Models;
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

    public static CellEntity MapFrom(DataItem cell)
    {
        ArgumentNullException.ThrowIfNull(cell, nameof(cell));
        ArgumentNullException.ThrowIfNull(cell.Value, nameof(cell.Value));

        return new CellEntity
        {
            Id = cell.Id,
            ColumnId = cell.DataFieldId,
            RowId = cell.DataRowId,
            Position = cell.Position,
            Value = cell.Value,
        };
    }

    public static DataItem MapTo(CellEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        ArgumentNullException.ThrowIfNull(entity.Value, nameof(entity.Value));

        return new DataItem
        {
            Id = entity.Id,
            DataFieldId = entity.ColumnId,
            DataRowId = entity.RowId,
            Position = entity.Position,
            Value = entity.Value,
        };
    }

    #endregion
}
