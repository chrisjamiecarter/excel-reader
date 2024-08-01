using ExcelReader.Data.Entities;

namespace ExcelReader.Data.Repositories;

public class DataFileRepository : SqliteRepository<DataFileEntity>, IDataFileRepository
{
    public DataFileRepository()
    {
        CreateTable();
    }
}
