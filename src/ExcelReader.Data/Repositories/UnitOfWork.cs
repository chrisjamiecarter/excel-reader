namespace ExcelReader.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(
        ISqliteDatabaseRepository sqliteDatabaseRepository, 
        IWorkbookRepository workbookRepository, 
        IWorksheetRepository worksheetRepository,
        IColumnRepository columnRepository,
        IRowRepository rowRepository,
        ICellRepository cellRepository)
    {
        Database = sqliteDatabaseRepository;
        Workbooks = workbookRepository;
        Worksheets = worksheetRepository;
        Columns = columnRepository;
        Rows = rowRepository;
        Cells = cellRepository;
    }

    public ISqliteDatabaseRepository Database { get; }

    public IWorkbookRepository Workbooks { get; }

    public IWorksheetRepository Worksheets { get; }

    public IColumnRepository Columns { get; }

    public IRowRepository Rows { get; }

    public ICellRepository Cells { get; }
}
