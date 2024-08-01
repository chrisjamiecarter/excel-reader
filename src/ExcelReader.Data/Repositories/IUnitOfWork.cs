namespace ExcelReader.Data.Repositories;

public interface IUnitOfWork
{
    IDataFileRepository DataFiles { get; }
}
