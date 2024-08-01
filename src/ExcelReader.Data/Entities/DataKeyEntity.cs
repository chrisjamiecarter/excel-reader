using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExcelReader.Data.Entities;

[Table("DataKey")]
public class DataKeyEntity
{ 
    #region Properties

    [Key]
    public int Id { get; set; }

    public int DataFileId { get; set; }

    public int Index { get; set; }
    
    public string Name { get; set; }

    #endregion
}
