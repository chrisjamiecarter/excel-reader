namespace ExcelReader.Models;

public class DataItem
{ 
    #region Properties

    public int Id { get; set; }

    public int DataFieldId { get; set; }

    public int DataRowId { get; set; }

    public int Position { get; set; }

    public string Value { get; set; } = string.Empty;

    #endregion
}
