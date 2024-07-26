
namespace DataReaderService;

/// <summary>
/// Сервис для чтения данных из файла.
/// </summary>
public interface IDataReader
{
    /// <summary>
    /// Попытаться прочитать данные из файла по указанному маршруту.
    /// </summary>
    /// <param name="filePath"> Путь к файлу. </param>
    /// <param name="format"> Формат файла. </param>
    /// <param name="data"> Полученные данные. </param>
    /// <returns> Успешность выполнения. </returns>
    public bool TryReadData(string filePath, FileFormatEnum format, out string[,] data);


    /// <summary>
    /// Попытаться прочитать данные из файла по указанному маршруту.
    /// </summary>
    /// <param name="filePath"> Путь к файлу. </param>
    /// <param name="extension"> Формат файла в текстовом виде. </param>
    /// <param name="data"> Полученные данные. </param>
    /// <returns> Успешность выполнения. </returns>
    public bool TryReadData(string filePath, string extension, out string[,] data);
}
