using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace ExcelReader.Models;

public class Row
{ 
    #region Properties

    public int Id { get; set; }

    public int WorksheetId { get; set; }

    public int Position { get; set; }

    public List<Cell> Cells { get; set; } = [];

    #endregion
    #region Methods

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append($"{nameof(Id)}={Id},");
        sb.Append($"{nameof(WorksheetId)}={WorksheetId},");
        sb.Append($"{nameof(Position)}={Position},");

        return sb.ToString();
    }

    #endregion
}
