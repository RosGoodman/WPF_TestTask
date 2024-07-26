#nullable disable

using Serilog;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using WPF_TestTask.DAL.Repositories;
using WPF_TestTask.Model.Models;
using WPF_TestTask.Model.ModelsDto;
using WPF_TestTask.ViewModel.IntermediateLogics.MapperService;

namespace WPF_TestTask.ViewModel.ViewModels.Windows;

/// <summary>  </summary>
public class MainWindowVM : NotifyPropertyChanged, INotifyPropertyChanged
{
    public delegate void NeedCloseApp();
    public event NeedCloseApp CloseApp;

    #region Feilds

    private readonly ILogger _logger;
    private readonly IElementRepository _elementRepository;
    private ObservableCollection<ElementDto> _items = new();
    private ElementDto _selectedItem;

    #endregion

    /////////////////////////////////////////////////////////////////////////////////

    #region Properties

    public string Title { get; set; } = "Тестовая программа";

    /// <summary>
    /// Коллекция элементов.
    /// </summary>
    public ObservableCollection<ElementDto> Items
    {
        get => new(_items.Where(e => !e.IsDeleted));
        set
        {
            _items = value;
            OnPropertyChanged(nameof(Items));
        }
    }

    /// <summary>
    /// Выбранный элемент.
    /// </summary>
    public ElementDto SelectedItem
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;
            OnPropertyChanged(nameof(SelectedItem));
        }
    }

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
    public MainWindowVM(ILogger logger,
        IElementRepository elementRepository)
    {
        _logger = logger;
        _elementRepository = elementRepository;
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
        _logger.Information($"{nameof(MainWindowVM)} >>> {nameof(SearchCommand_Execute)}. Поиск.");

        var entitiesDb = _elementRepository.GetAll();
        if (entitiesDb is null || entitiesDb.Count() == 0)
            Items = new();

        Items = new(StandartMapper.Map<Element, ElementDto>(entitiesDb));
    }



    /// <summary>
    /// Выполнить команду "Загрузить данные из файла".
    /// </summary>
    /// <param name="param"></param>
    private void DownloadCommand_Execute(object param)
    {
        _logger.Information($"{nameof(MainWindowVM)} >>> {nameof(DownloadCommand_Execute)}. Загрузить данные из файла.");
    }



    /// <summary>
    /// Выполнить команду "Сохранить изменения".
    /// </summary>
    /// <param name="param"></param>
    private void SaveCommand_Execute(object param)
    {
        _logger.Information($"{nameof(MainWindowVM)} >>> {nameof(SaveCommand_Execute)}. Сохранить изменения.");
    }



    /// <summary>
    /// Проверить возможность выполнить команду "Удалить выбранный элемент".
    /// </summary>
    /// <param name="param"> Параметр. </param>
    /// <returns> Результат операции. </returns>
    private bool DeleteSelectedCommand_CanExecute(object param)
    {
        if(_selectedItem is null)
            return false;
        return true;
    }

    /// <summary>
    /// Выполнить команду "Удалить выбранный элемент".
    /// </summary>
    /// <param name="param"></param>
    private void DeleteSelectedCommand_Execute(object param)
    {
        _logger.Information($"{nameof(MainWindowVM)} >>> {nameof(DeleteSelectedCommand_Execute)}. Удалить выбранный элемент.");

        SelectedItem.IsDeleted = true;
        OnPropertyChanged(nameof(Items));
    }


    #endregion

    /////////////////////////////////////////////////////////////////////////////////

    #region ViewModelMethods


    #endregion
}