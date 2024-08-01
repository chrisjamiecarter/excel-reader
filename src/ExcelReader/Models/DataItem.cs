using System.ComponentModel.DataAnnotations;

namespace ExcelReader.Models;

public class DataItem
{ 
    #region Properties

    public int Id { get; set; }

    public int DataKeyId { get; set; }

    public int Index { get; set; }
    
    public string Name { get; set; }

    #endregion
}
