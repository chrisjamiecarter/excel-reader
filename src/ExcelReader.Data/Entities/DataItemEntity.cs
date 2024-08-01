using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExcelReader.Data.Entities;

[Table("DataItem")]
public class DataItemEntity
{ 
    #region Properties

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int Id { get; set; }

    public int DataKeyId { get; set; }

    public int Index { get; set; }
    
    public string Name { get; set; }

    #endregion
}
