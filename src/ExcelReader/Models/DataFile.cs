namespace ExcelReader.Models;

public class DataFile
{
    #region Properties

    public int Id { get; set; }
    
    public string Name { get; set; } = "";

    public string Extension { get; set; } = "";

    public long Size { get; set; }

    public List<DataSheet> DataSheets { get; set; } = [];

    #endregion
}
