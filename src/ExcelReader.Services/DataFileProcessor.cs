using ExcelReader.Models;
using Microsoft.Extensions.Logging;

namespace ExcelReader.Services;

public class DataFileProcessor : IDataFileProcessor
{
    #region Fields

    private readonly ILogger<DataFileProcessor> _logger;
    private readonly ICsvDataFileReader _csvDataFileReader;
    private readonly IExcelDataFileReader _excelDataFileReader;

    #endregion
    #region Constructors

    public DataFileProcessor(ILogger<DataFileProcessor> logger, ICsvDataFileReader csvDataFileReader, IExcelDataFileReader excelDataFileReader)
    {
        _logger = logger;
        _csvDataFileReader = csvDataFileReader;
        _excelDataFileReader = excelDataFileReader;
    }

    #endregion
    #region Methods

    public DataFile ProcessFile(FileInfo fileInfo)
    {
        ArgumentNullException.ThrowIfNull(fileInfo, nameof(fileInfo));

        switch (fileInfo.Extension.ToLower())
        {
            case ".csv":
                // SupportedFileExtension.CSV;
                return _csvDataFileReader.ReadDataFile(fileInfo);
            case ".xlsx":
                //SupportedFileExtension.XLSX;
                return _excelDataFileReader.ReadDataFile(fileInfo);
            default:
                throw new InvalidOperationException($"Unsupported file type: {fileInfo.Extension}");
        }
    }

    #endregion
}
