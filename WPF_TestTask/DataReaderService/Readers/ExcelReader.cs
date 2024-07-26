#nullable disable

using ExcelDataReader;
using System.Diagnostics;

namespace DataReaderService.Excel;

internal static class ExcelReader
{
    /// <summary> Читать данные. </summary>
    /// <param name="filePath"> Путь к файлу. </param>
    /// <param name="fileFormat"> Расширение файла. </param>
    /// <param name="table"> Массив данных, полученных из файла. </param>
    /// <returns> Результат работы метода. </returns>
    internal static bool TryRead(string filePath, FileFormatEnum fileFormat, out string[,] table)
    {
        table = null;

        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        try
        {
            var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

            IExcelDataReader reader;
            if(fileFormat == FileFormatEnum.Excel)
                reader = ExcelReaderFactory.CreateReader(stream);
            else 
                reader = ExcelReaderFactory.CreateCsvReader(stream);

            var config = new ExcelDataSetConfiguration
            {
                ConfigureDataTable = _ => new ExcelDataTableConfiguration { UseHeaderRow = false }
            };

            var result = reader.AsDataSet(config);
            var dataTable = result.Tables[0];

            int col = dataTable.Columns.Count;
            int rows = dataTable.Rows.Count;

            table = new string[rows - 1, col];

            //перебор ячеек
            for (int i = 1; i <= dataTable.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    //Console.WriteLine($"{dataTable.Columns[j]} {dataTable.Rows[i][j]}");
                    table[i - 1, j] = dataTable.Rows[i][j].ToString()!;
                }
            }
            stream.Close();
            return true;
        }
        catch (IOException ex)
        {
            Debug.Print(ex.Message + "\n\n" +
                "Проверьте закрыт ли выбранный файл excel.");
        }
        catch (Exception ex)
        {
            Debug.Print(ex.Message);
        }

        return false;
    }
}
