using DataReaderService.Excel;

namespace DataReaderService;

/// <inheritdoc/>
public class DataReader : IDataReader
{
    private static readonly string[] _excelExctensions = [".xls", ".xlsx", ".xlsm"];
    private static readonly string _csvExctensions = ".csv";

    /// <inheritdoc/>
    public bool TryReadData(string filePath, FileFormatEnum format, out string[,] data)
    {
        data = null!;
        if(filePath == string.Empty)
            return false;

        switch (format)
        {
            case FileFormatEnum.Excel:
                return ExcelReader.TryRead(filePath, FileFormatEnum.Excel, out data);
            case FileFormatEnum.CSV:
                return ExcelReader.TryRead(filePath, FileFormatEnum.CSV, out data);
        }

        return false;
    }

    /// <inheritdoc/>
    public bool TryReadData(string filePath, string extension, out string[,] data)
    {
        data = null!;
        if(!TryGetFileFormat(extension, out var format))
            return false;

        return TryReadData(filePath, format, out data);
    }

    /// <summary>
    /// Попытаться преобразовать строчное отображение расширения файла в перечисление <see cref="FileFormatEnum"/>
    /// </summary>
    /// <param name="extension"> Расширение файла. </param>
    /// <param name="fileFormat"> Расширение файла в формате перечисления. </param>
    /// <returns> Результат работы метода. </returns>
    private static bool TryGetFileFormat(string extension, out FileFormatEnum fileFormat)
    {
        fileFormat = FileFormatEnum.Excel;
        extension = extension.ToLower();

        if(extension == _csvExctensions)
        {
            fileFormat = FileFormatEnum.CSV;
            return true;
        }

        if(_excelExctensions.Contains(extension))
        {
            fileFormat = FileFormatEnum.Excel;
            return true;
        }

        return false;
    }
}
