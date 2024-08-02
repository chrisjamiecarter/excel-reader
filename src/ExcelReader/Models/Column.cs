using static OfficeOpenXml.ExcelErrorValue;
using System.Text;

namespace ExcelReader.Models;

public class Column
{ 
    #region Properties

    public int Id { get; set; }

    public int WorksheetId { get; set; }

    public int Position { get; set; }
    
    public string Name { get; set; } = string.Empty;

    #endregion
    #region Methods

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append($"{nameof(Id)}={Id},");
        sb.Append($"{nameof(WorksheetId)}={WorksheetId},");
        sb.Append($"{nameof(Position)}={Position},");
        sb.Append($"{nameof(Name)}={Name},");

        return sb.ToString();
    }

    #endregion
}
