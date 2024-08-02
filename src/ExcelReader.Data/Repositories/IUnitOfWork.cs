namespace ExcelReader.Data.Repositories;

public interface IUnitOfWork
{
    IWorkbookRepository Workbooks { get; }
    IWorksheetRepository Worksheets { get; }
    ISqliteDatabaseRepository Database { get; }
    IColumnRepository Columns { get; }
    IRowRepository Rows { get; }
    ICellRepository Cells { get; }
}
