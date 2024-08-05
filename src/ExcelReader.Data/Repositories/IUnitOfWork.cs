namespace ExcelReader.Data.Repositories;

public interface IUnitOfWork
{
    IDataFileRepository DataFiles { get; }
    IDataSheetRepository DataSheets { get; }
    IDatabaseRepository Database { get; }
    IDataFieldRepository DataFields { get; }
    IDataSheetRowRepository DataSheetRows { get; }
    IDataItemRepository DataItems { get; }
}
