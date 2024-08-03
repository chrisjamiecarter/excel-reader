namespace ExcelReader.Models;

public class DataField
{ 
    #region Properties

    public int Id { get; set; }

    public int DataSheetId { get; set; }

    public int Position { get; set; }
    
    public string Name { get; set; } = string.Empty;

    public List<DataItem> DataItems { get; set; } = [];

    #endregion
}
