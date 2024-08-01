using System.ComponentModel.DataAnnotations;

namespace ExcelReader.Models;

public class DataKey
{ 
    #region Properties

    public int Id { get; set; }

    public int DataFileId { get; set; }

    public int Index { get; set; }
    
    public string Name { get; set; }

    #endregion
}
