namespace WPF_TestTask.ViewModel.Services;

internal static class FileDialog
{
    /// <summary> Диалог выбора файлов Excel. </summary>
    /// <returns> Полный путь файла. </returns>
    public static string OpenFileDialog()
    {
        Microsoft.Win32.OpenFileDialog openFileDialog = new()
        {
            Filter = "Excel|*.xlsx;*.xlsm;*.xls;*.csv"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            return openFileDialog.FileName;
        }
        return "";
    }
}
