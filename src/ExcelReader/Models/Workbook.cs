using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExcelReader.Models;

public class Workbook
{
    #region Properties

    public int Id { get; set; }
    
    public string Name { get; set; } = "";

    public string Extension { get; set; } = "";

    public long Size { get; set; }

    public List<Worksheet> Worksheets { get; set; } = [];

    #endregion
    #region Methods

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append($"{nameof(Id)}={Id},");
        sb.Append($"{nameof(Name)}={Name},");
        sb.Append($"{nameof(Extension)}={Extension},");
        sb.Append($"{nameof(Size)}={Size},");

        return sb.ToString();
    }

    #endregion
}
