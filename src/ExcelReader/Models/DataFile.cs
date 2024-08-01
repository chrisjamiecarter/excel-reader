using System.ComponentModel.DataAnnotations;

namespace ExcelReader.Models;

public class DataFile
{
    #region Properties

    public int Id { get; set; }

    public string DirectoryPath { get; set; }
    
    public string Name { get; set; }

    public string Extension { get; set; }

    public long Size { get; set; }

    #endregion
}
