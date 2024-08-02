using System.Data.SQLite;
using Dapper;
using ExcelReader.Data.Entities;

namespace ExcelReader.Data.Repositories;

public class WorksheetRepository : SqliteRepository<WorksheetEntity>, IWorksheetRepository
{
    public async Task<IEnumerable<WorksheetEntity>> GetByWorkbookIdAsync(int workbookId)
    {
        string query = $"SELECT * FROM Worksheet WHERE WorkbookId = '{workbookId}';";

        using var connection = new SQLiteConnection(ConnectionString);

        return await connection.QueryAsync<WorksheetEntity>(query);
    }
}
