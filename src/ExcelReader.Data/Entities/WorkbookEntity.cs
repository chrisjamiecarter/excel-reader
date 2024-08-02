using ExcelReader.Models;
using SQLite;

namespace ExcelReader.Data.Entities;

[Table("Workbook")]
public class WorkbookEntity
{
    #region Properties

    [PrimaryKey]
    [NotNull]
    [AutoIncrement]
    public int Id { get; set; }

    [NotNull]
    public string DirectoryPath { get; set; } = string.Empty;

    [NotNull]
    public string Name { get; set; } = string.Empty;

    [NotNull]
    public string Extension { get; set; } = string.Empty;

    [NotNull]
    public long Size { get; set; }

    #endregion
    #region Methods

    public static WorkbookEntity MapFrom(Workbook workbook)
    {
        ArgumentNullException.ThrowIfNull(workbook, nameof(workbook));
        ArgumentException.ThrowIfNullOrWhiteSpace(workbook.DirectoryPath, nameof(workbook.DirectoryPath));
        ArgumentException.ThrowIfNullOrWhiteSpace(workbook.Name, nameof(workbook.Name));
        ArgumentException.ThrowIfNullOrWhiteSpace(workbook.Extension, nameof(workbook.Extension));

        return new WorkbookEntity
        {
            Id = workbook.Id,
            DirectoryPath = workbook.DirectoryPath,
            Name = workbook.Name,
            Extension = workbook.Extension,
            Size = workbook.Size
        };
    }

    public static Workbook MapTo(WorkbookEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        ArgumentException.ThrowIfNullOrWhiteSpace(entity.DirectoryPath, nameof(entity.DirectoryPath));
        ArgumentException.ThrowIfNullOrWhiteSpace(entity.Name, nameof(entity.Name));
        ArgumentException.ThrowIfNullOrWhiteSpace(entity.Extension, nameof(entity.Extension));

        return new Workbook
        {
            Id = entity.Id,
            DirectoryPath = entity.DirectoryPath,
            Name = entity.Name,
            Extension = entity.Extension,
            Size = entity.Size
        };
    }

    #endregion
}
