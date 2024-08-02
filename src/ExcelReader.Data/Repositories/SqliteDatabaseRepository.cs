using System.Data.SQLite;
using ExcelReader.Configurations;
using Microsoft.Extensions.Options;

namespace ExcelReader.Data.Repositories;

public class SqliteDatabaseRepository : ISqliteDatabaseRepository
{
    #region Fields

    private readonly ApplicationOptions _options;
    private readonly IWorkbookRepository _workbookRepository;
    private readonly IWorksheetRepository _worksheetRepository;
    private readonly IColumnRepository _columnRepository;
    private readonly IRowRepository _rowRepository;
    private readonly ICellRepository _cellRepository;

    #endregion
    #region Constructors

    public SqliteDatabaseRepository(
        IOptions<ApplicationOptions> options,
        IWorkbookRepository workbookRepository, 
        IWorksheetRepository worksheetRepository,
        IColumnRepository columnRepository,
        IRowRepository rowRepository,
        ICellRepository cellRepository)
    {
        _options = options.Value;
        _workbookRepository = workbookRepository;
        _worksheetRepository = worksheetRepository;
        _columnRepository = columnRepository;
        _rowRepository = rowRepository;
        _cellRepository = cellRepository;
    }

    #endregion
    #region Properties

    private string ConnectionString => $"Data Source={FileName}";

    private string FileName => Path.ChangeExtension(_options.DatabaseName, _options.DatabaseExtension);

    private string FilePath => Path.GetFullPath(FileName);

    #endregion
    #region Methods

    public void EnsureCreated()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nameof(FileName));
        
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
        ArgumentException.ThrowIfNullOrWhiteSpace(nameof(FileName));

        if (File.Exists(FilePath))
        {
            File.Delete(FilePath);
        }
    }

    #endregion
}
