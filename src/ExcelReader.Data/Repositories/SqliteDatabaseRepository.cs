using System.Data.SQLite;

namespace ExcelReader.Data.Repositories;

public class SqliteDatabaseRepository : ISqliteDatabaseRepository
{
    #region Constants

    private readonly string _databaseName = "ExcelReader";

    private readonly string _databaseExtension = ".db";
    private readonly IWorkbookRepository _workbookRepository;
    private readonly IWorksheetRepository _worksheetRepository;
    private readonly IColumnRepository _columnRepository;
    private readonly IRowRepository _rowRepository;
    private readonly ICellRepository _cellRepository;

    #endregion
    #region Constructors

    public SqliteDatabaseRepository(
        IWorkbookRepository workbookRepository, 
        IWorksheetRepository worksheetRepository,
        IColumnRepository columnRepository,
        IRowRepository rowRepository,
        ICellRepository cellRepository)
    {
        _workbookRepository = workbookRepository;
        _worksheetRepository = worksheetRepository;
        _columnRepository = columnRepository;
        _rowRepository = rowRepository;
        _cellRepository = cellRepository;
    }

    #endregion
    #region Properties

    private string ConnectionString => $"Data Source={FileName}";

    private string FileName => Path.ChangeExtension(_databaseName, _databaseExtension);

    private string FilePath => Path.GetFullPath(FileName);

    #endregion
    #region Methods

    public void EnsureCreated()
    {
        using var connection = new SQLiteConnection(ConnectionString);
        connection.Open();
        connection.Close();

        _workbookRepository.CreateTable();
        _worksheetRepository.CreateTable();
        _columnRepository.CreateTable();
        _rowRepository.CreateTable();
        _cellRepository.CreateTable();
    }

    public void EnsureDeleted()
    {
        if (File.Exists(FilePath))
        {
            File.Delete(FilePath);
        }
    }

    #endregion
}
