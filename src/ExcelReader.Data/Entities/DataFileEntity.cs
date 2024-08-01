using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExcelReader.Models;

namespace ExcelReader.Data.Entities;

[Table("DataFile")]
public class DataFileEntity
{
    #region Properties

    [Key]
    public int Id { get; set; }

    public string DirectoryPath { get; set; }
    
    public string Name { get; set; }

    public string Extension { get; set; }

    public long Size { get; set; }

    #endregion
    #region Methods

    public static DataFileEntity MapFrom(DataFile dataFile)
    {
        ArgumentNullException.ThrowIfNull(dataFile, nameof(dataFile));
        ArgumentException.ThrowIfNullOrWhiteSpace(dataFile.DirectoryPath, nameof(dataFile.DirectoryPath));
        ArgumentException.ThrowIfNullOrWhiteSpace(dataFile.Name, nameof(dataFile.Name));
        ArgumentException.ThrowIfNullOrWhiteSpace(dataFile.Extension, nameof(dataFile.Extension));

        return new DataFileEntity
        {
            Id = dataFile.Id,
            DirectoryPath = dataFile.DirectoryPath,
            Name = dataFile.Name,
            Extension = dataFile.Extension,
            Size = dataFile.Size
        };
    }

    public static DataFile MapTo(DataFileEntity dataFile)
    {
        ArgumentNullException.ThrowIfNull(dataFile, nameof(dataFile));
        ArgumentException.ThrowIfNullOrWhiteSpace(dataFile.DirectoryPath, nameof(dataFile.DirectoryPath));
        ArgumentException.ThrowIfNullOrWhiteSpace(dataFile.Name, nameof(dataFile.Name));
        ArgumentException.ThrowIfNullOrWhiteSpace(dataFile.Extension, nameof(dataFile.Extension));

        return new DataFile
        {
            Id = dataFile.Id,
            DirectoryPath = dataFile.DirectoryPath,
            Name = dataFile.Name,
            Extension = dataFile.Extension,
            Size = dataFile.Size
        };
    }

    #endregion
}
