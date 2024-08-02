using System.Drawing;
using System.Text;

namespace ExcelReader.Models;

public class Worksheet
{
    #region Properties

    public int Id { get; set; }

    public int WorkbookId { get; set; }

    public int Position { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<Column> Columns { get; set; } = [];
    
    public List<Row> Rows { get; set; } = [];

    #endregion
    #region Methods

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append($"{nameof(Id)}={Id},");
        sb.Append($"{nameof(WorkbookId)}={WorkbookId},");
        sb.Append($"{nameof(Position)}={Position},");
        sb.Append($"{nameof(Name)}={Name},");
        
        return sb.ToString();
    }

    #endregion
}
