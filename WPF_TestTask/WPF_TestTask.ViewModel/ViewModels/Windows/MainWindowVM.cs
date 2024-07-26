#nullable disable

using Serilog;
using System.ComponentModel;
using System.Windows;

namespace WPF_TestTask.ViewModel.ViewModels.Windows;

/// <summary>  </summary>
public class MainWindowVM : NotifyPropertyChanged, INotifyPropertyChanged
{
    public delegate void NeedCloseApp();
    public event NeedCloseApp CloseApp;

    #region Feilds

    private readonly ILogger _logger;

    #endregion

    /////////////////////////////////////////////////////////////////////////////////

    #region Properties

    public string Title { get; set; } = "Тестовая программа";

    #endregion

    /////////////////////////////////////////////////////////////////////////////////

    #region CommandProperties

    /// <summary>
    /// Команда. Поиск.
    /// </summary>
    public RelayCommand SearchCommand { get; private set; }

    /// <summary>
    /// Команда. Загрузить данные из файла.
    /// </summary>
    public RelayCommand DownloadCommand { get; private set; }

    /// <summary>
    /// Команда. Сохранить изменения.
    /// </summary>
    public RelayCommand SaveCommand { get; private set; }

    /// <summary>
    /// Команда. Удалить выбранный элемент.
    /// </summary>
    public RelayCommand DeleteSelectedCommand { get; private set; }

    #endregion

    /////////////////////////////////////////////////////////////////////////////////

    /// <summary> Ctor. </summary>
    public MainWindowVM(ILogger logger)
    {
        _logger = logger;
        _logger.Information($"Логгер встроен в {nameof(MainWindowVM)}");

        SearchCommand = new RelayCommand(SearchCommand_Execute);
        DownloadCommand = new RelayCommand(DownloadCommand_Execute);
        SaveCommand = new RelayCommand(SaveCommand_Execute);
        DeleteSelectedCommand = new RelayCommand(DeleteSelectedCommand_Execute, DeleteSelectedCommand_CanExecute);
    }

    /////////////////////////////////////////////////////////////////////////////////

    #region CommandMethods



    /// <summary>
    /// Выполнить команду "Поиск".
    /// </summary>
    /// <param name="param"></param>
    private void SearchCommand_Execute(object param)
    {
        _logger.Debug($"{nameof(MainWindowVM)} >>> {nameof(SearchCommand_Execute)}. Поиск.");
    }



    /// <summary>
    /// Выполнить команду "Загрузить данные из файла".
    /// </summary>
    /// <param name="param"></param>
    private void DownloadCommand_Execute(object param)
    {
        _logger.Debug($"{nameof(MainWindowVM)} >>> {nameof(DownloadCommand_Execute)}. Загрузить данные из файла.");
    }



    /// <summary>
    /// Выполнить команду "Сохранить изменения".
    /// </summary>
    /// <param name="param"></param>
    private void SaveCommand_Execute(object param)
    {
        _logger.Debug($"{nameof(MainWindowVM)} >>> {nameof(SaveCommand_Execute)}. Сохранить изменения.");
    }



    /// <summary>
    /// Проверить возможность выполнить команду "Удалить выбранный элемент".
    /// </summary>
    /// <param name="param"> Параметр. </param>
    /// <returns> Результат операции. </returns>
    private bool DeleteSelectedCommand_CanExecute(object param) => true;

    /// <summary>
    /// Выполнить команду "Удалить выбранный элемент".
    /// </summary>
    /// <param name="param"></param>
    private void DeleteSelectedCommand_Execute(object param)
    {
        _logger.Debug($"{nameof(MainWindowVM)} >>> {nameof(DeleteSelectedCommand_Execute)}. Удалить выбранный элемент.");
    }


    #endregion

    /////////////////////////////////////////////////////////////////////////////////

    #region ViewModelMethods


    #endregion
}