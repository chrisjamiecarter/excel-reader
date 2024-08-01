using ExcelReader.Data.Entities;

namespace ExcelReader.Data.Repositories;

public class DataFileRepository : DapperRepository<DataFileEntity>, IDataFileRepository
{
    public DataFileRepository()
    {
        CreateTable();
    }
}
