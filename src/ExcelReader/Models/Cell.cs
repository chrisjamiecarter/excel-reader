using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Text;
using System.Xml.Linq;

namespace ExcelReader.Models;

public class Cell
{ 
    #region Properties

    public int Id { get; set; }

    public int ColumnId { get; set; }

    public int RowId { get; set; }

    public int Position { get; set; }

    public string Value { get; set; } = string.Empty;

    #endregion
    #region Methods

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append($"{nameof(Id)}={Id},");
        sb.Append($"{nameof(ColumnId)}={ColumnId},");
        sb.Append($"{nameof(RowId)}={RowId},");
        sb.Append($"{nameof(Position)}={Position},");
        sb.Append($"{nameof(Value)}={Value},");

        return sb.ToString();
    }

    #endregion
}
