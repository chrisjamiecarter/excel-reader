namespace ExcelReader.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(IDataFileRepository dataFileRepository)
    {
        DataFiles = dataFileRepository;
    }

    public IDataFileRepository DataFiles { get; set; }

}
