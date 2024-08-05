namespace ExcelReader.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(
        IDatabaseRepository databaseRepository, 
        IDataFileRepository dataFileRepository, 
        IDataSheetRepository dataSheetRepository,
        IDataFieldRepository dataFieldRepository,
        IDataSheetRowRepository dataSheetRowRepository,
        IDataItemRepository dataItemRepository)
    {
        Database = databaseRepository;
        DataFiles = dataFileRepository;
        DataSheets = dataSheetRepository;
        DataFields = dataFieldRepository;
        DataSheetRows = dataSheetRowRepository;
        DataItems = dataItemRepository;
    }

    public IDatabaseRepository Database { get; }

    public IDataFileRepository DataFiles { get; }

    public IDataSheetRepository DataSheets { get; }

    public IDataFieldRepository DataFields { get; }

    public IDataSheetRowRepository DataSheetRows { get; }

    public IDataItemRepository DataItems { get; }
}
