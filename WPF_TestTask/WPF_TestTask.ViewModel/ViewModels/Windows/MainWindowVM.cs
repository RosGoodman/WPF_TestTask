﻿#nullable disable

using BitmapCreatorService;
using DataReaderService;
using Serilog;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using WPF_TestTask.DAL.DataContext;
using WPF_TestTask.DAL.Repositories;
using WPF_TestTask.Model.Models;
using WPF_TestTask.Model.ModelsDto;
using WPF_TestTask.ViewModel.Services;
using WPF_TestTask.ViewModel.Services.Converters;
using WPF_TestTask.ViewModel.Services.IntermediateLogics.MapperService;
using WPF_TestTask.ViewModel.ViewModels.Windows.DialogWindows;
using System;

namespace WPF_TestTask.ViewModel.ViewModels.Windows;

/// <summary> MainWindow view model. </summary>
public class MainWindowVM : NotifyPropertyChanged, INotifyPropertyChanged
{
    public delegate void NeedCloseApp();
    public event NeedCloseApp CloseApp;

    #region Feilds

    private readonly ILogger _logger;
    private readonly IElementRepository _elementRepository;
    private readonly IContextDB _contextDB;
    private readonly IDataReader _dataReader;
    private readonly IMessageBoxVM _messageBoxVM;
    private readonly IBitmapCreator _bitmapCreator;

    private ObservableCollection<string> _selectedItemProperties = new();
    private ObservableCollection<ElementDto> _items = new();
    private ElementDto _selectedItem;
    private ImageSource _image;

    #endregion

    /////////////////////////////////////////////////////////////////////////////////

    #region Properties

    /// <summary>
    /// Title.
    /// </summary>
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
            if(_selectedItem == value) return;
            _selectedItem = value;
            OnPropertyChanged(nameof(SelectedItem));
            CreateImage();
            SetPropertiesColl();
        }
    }

    /// <summary>
    /// Изображение.
    /// </summary>
    public ImageSource Image
    {
        get => _image;
        private set
        {
            _image = value;
            OnPropertyChanged(nameof(Image));
        }
    }

    /// <summary>
    /// Список характеристик выбранного элемента.
    /// </summary>
    public ObservableCollection<string> SelectedItemProperties
    {
        get => _selectedItemProperties;
        private set
        {
            _selectedItemProperties = value;
            OnPropertyChanged(nameof(SelectedItemProperties));
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
        IElementRepository elementRepository,
        IContextDB contextDB,
        IDataReader dataReader,
        IMessageBoxVM messageBoxVM,
        IBitmapCreator bitmapCreator)
    {
        _logger = logger;
        _elementRepository = elementRepository;
        _contextDB = contextDB;
        _dataReader = dataReader;
        _messageBoxVM = messageBoxVM;
        _bitmapCreator = bitmapCreator;
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
    /// <param name="param"> Параметр. </param>
    private void SearchCommand_Execute(object param)
    {
        _logger.Information($"{nameof(MainWindowVM)} >>> {nameof(SearchCommand_Execute)}. Поиск.");

        var entitiesDb = _elementRepository.GetAll();
        if (entitiesDb is null || entitiesDb.Count() == 0)
        {
            Items = new();
            _messageBoxVM.AskTheQuestion("Ничего не найдено.",
                "Поиск",
                MessageService.Enums.MessageBoxButtonEnum.OK);

            return;
        }

        _items = new(StandartMapper.Map<Element, ElementDto>(entitiesDb));
        Indexator.SetIndexes(_items);
        OnPropertyChanged(nameof(Items));

        _messageBoxVM.AskTheQuestion("Готово.",
                "Поиск",
                MessageService.Enums.MessageBoxButtonEnum.OK);
    }



    /// <summary>
    /// Выполнить команду "Загрузить данные из файла".
    /// </summary>
    /// <param name="param"> Параметр. </param>
    private void DownloadCommand_Execute(object param)
    {
        _logger.Information($"{nameof(MainWindowVM)} >>> {nameof(DownloadCommand_Execute)}. Загрузить данные из файла.");

        var filePath = FileDialog.OpenFileDialog();
        if(filePath == string.Empty)
            return;

        var fileinfo = new FileInfo(filePath);

        if(!_dataReader.TryReadData(filePath, fileinfo.Extension, out string[,] data))
            return;

        var convertedEntities = Indexator.SetIndexes(
            ElementsConverter.Convert(data));

        Items = new(convertedEntities);
    }



    /// <summary>
    /// Выполнить команду "Сохранить изменения".
    /// </summary>
    /// <param name="param"> Параметр. </param>
    private void SaveCommand_Execute(object param)
    {
        _logger.Information($"{nameof(MainWindowVM)} >>> {nameof(SaveCommand_Execute)}. Сохранить изменения.");

        if (!CheckForChanges(_items))
        {
            _messageBoxVM.AskTheQuestion("Готово", "Сохранение", MessageService.Enums.MessageBoxButtonEnum.OK);
            return;
        }

        List<ElementDto> entitiesForDelete = new();
        List<ElementDto> entitiesForAdd = new();
        List<ElementDto> entitiesForUpdate = new();
        ElementDto currentItem = null;

        for (int i = 0; i < _items.Count; i++)
        {
            currentItem = _items[i];
            if (currentItem.IsDeleted && currentItem.Id != Guid.Empty)
            {
                entitiesForDelete.Add(currentItem);
                continue;
            }

            if(currentItem.Id == Guid.Empty)
            {
                entitiesForAdd.Add(currentItem);
                continue;
            }

            if (currentItem.IsChanged)
                entitiesForUpdate.Add(currentItem);
        }

        if(DeleteElements(entitiesForDelete) && AddElements(entitiesForAdd) && UpdateEntities(entitiesForUpdate))
        {
            _messageBoxVM.AskTheQuestion("Готово", "Сохранение", MessageService.Enums.MessageBoxButtonEnum.OK);
            return;
        }
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
    /// <param name="param"> Параметр. </param>
    private void DeleteSelectedCommand_Execute(object param)
    {
        _logger.Information($"{nameof(MainWindowVM)} >>> {nameof(DeleteSelectedCommand_Execute)}. Удалить выбранный элемент.");

        _messageBoxVM.AskTheQuestion("Пометить выбранный элемент на удаление?",
                "Удаление",
                MessageService.Enums.MessageBoxButtonEnum.YesNo);

        if (_messageBoxVM.ButtonResult == MessageService.Enums.MessageBoxResultEnum.No)
            return;

        SelectedItem.IsDeleted = true;
        OnPropertyChanged(nameof(Items));
    }


    #endregion

    /////////////////////////////////////////////////////////////////////////////////

    #region ViewModelMethods

    /// <summary>
    /// Проверить коллекцию на наличие несохраненных изменений.
    /// </summary>
    /// <param name="entities"> Проверяемые экземпляры </param>
    /// <returns> Результат проверки (true - существуют несохраненные изменения). </returns>
    private bool CheckForChanges(ObservableCollection<ElementDto> entities)
    {
        if(entities is null)
            return false;

        for (int i = 0; i < entities.Count; i++)
            if (entities[i].IsChanged || entities[i].IsDeleted)
                return true;

        return false;
    }

    #region SaveChanges

    /// <summary>
    /// Удалить эелементы из БД.
    /// </summary>
    /// <param name="entities"> Список удаляемых элементов. </param>
    /// <returns> Элементы успешно удалены. </returns>
    private bool DeleteElements(List<ElementDto> entities)
    {
        if (entities is null || entities.Count == 0)
            return true;

        var transaction = _contextDB.ContextBeginTransaction();

        try
        {
            for (int i = 0; i < entities.Count; i++)
                _elementRepository.Delete(entities[i].Id);

            transaction.Commit();
            return true;
        }
        catch (Exception ex)
        {
            string errMsg = "Возникла непредвиденная ошибка при попытке удаления элементов.";

            _logger.Error($"{nameof(MainWindowVM)} >>> {nameof(DeleteElements)} >>> {errMsg} >>> {ex.Message}");
            _messageBoxVM.AskTheQuestion($"{errMsg}", "Ошибка", MessageService.Enums.MessageBoxButtonEnum.OK);
            transaction.Rollback();
            return false;
        }
    }

    /// <summary>
    /// Добавить элементы в БД.
    /// </summary>
    /// <param name="entities"> Список добавляемых элементов. </param>
    /// <returns> Элементы успешно добавлены. </returns>
    private bool AddElements(List<ElementDto> entities)
    {
        if (entities is null || entities.Count() == 0)
            return true;

        var entitiesDb = StandartMapper.Map<ElementDto, Element>(entities);
        var transaction = _contextDB.ContextBeginTransaction();

        try
        {
            for (int i = 0; i < entitiesDb.Count; i++)
                _elementRepository.Add(entitiesDb[i]);

            transaction.Commit();
            return true;
        }
        catch (Exception ex)
        {
            string errMsg = "Возникла непредвиденная ошибка при попытке сохранения элементов.";

            _logger.Error($"{nameof(MainWindowVM)} >>> {nameof(DeleteElements)} >>> {errMsg} >>> {ex.Message}");
            _messageBoxVM.AskTheQuestion($"{errMsg}", "Ошибка", MessageService.Enums.MessageBoxButtonEnum.OK);
            transaction.Rollback();
            return false;
        }
    }

    /// <summary>
    /// Обновить элементы в БД.
    /// </summary>
    /// <param name="entities"> Список обновляемых элементов. </param>
    /// <returns> Элементы успешно обновлены. </returns>
    private bool UpdateEntities(List<ElementDto> entities)
    {
        if (entities is null || entities.Count() == 0)
            return true;

        var entitiesDb = StandartMapper.Map<ElementDto, Element>(entities);
        var transaction = _contextDB.ContextBeginTransaction();

        try
        {
            for (int i = 0; i < entitiesDb.Count; i++)
                _elementRepository.UpdateDetached(entitiesDb[i]);

            transaction.Commit();
            return true;
        }
        catch (Exception ex)
        {
            string errMsg = "Возникла непредвиденная ошибка при попытке сохранения изменений элементов.";

            _logger.Error($"{nameof(MainWindowVM)} >>> {nameof(DeleteElements)} >>> {errMsg} >>> {ex.Message}");
            _messageBoxVM.AskTheQuestion($"{errMsg}", "Ошибка", MessageService.Enums.MessageBoxButtonEnum.OK);
            transaction.Rollback();
            return false;
        }
    }

    #endregion

    #region Image

    /// <summary>
    /// Создать изображение для выбранного элемента.
    /// </summary>
    private void CreateImage()
    {
        if(_selectedItem is null)
        {
            Image = ImageSourceFromBitmap(_bitmapCreator
                .CreateAxis(20, 12)
                .GetBitmap());
            return;
        }

        var bitmapAxis = _bitmapCreator
            .CreateAxis(20, 12)
            .DrawFigure(BitmapCreatorService.Figures.FiguresEnum.Rectangle,
                        _selectedItem.Distance,
                        _selectedItem.Angle,
                        _selectedItem.Width,
                        _selectedItem.Height)
            .GetBitmap();

        Image = ImageSourceFromBitmap(bitmapAxis);
    }

    public ImageSource ImageSourceFromBitmap(Bitmap bmp)
    {
        var handle = bmp.GetHbitmap();
        try
        {
            return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    #endregion

    /// <summary>
    /// Назначить отображаемую коллекцию характеристик.
    /// </summary>
    private void SetPropertiesColl()
    {
        SelectedItemProperties = new();

        if (_selectedItem is null)
            return;

        Type elementType = typeof(ElementDto);

        //Name
        var nameProperty = elementType.GetProperty(nameof(ElementDto.Name));
        var nameDN = DisplayNameReader.GetDisplayName(nameProperty);

        _selectedItemProperties.Add($"{nameDN}: {_selectedItem.Name}");

        //Distance
        var distanceProperty = elementType.GetProperty(nameof(ElementDto.Distance));
        var distanceDN = DisplayNameReader.GetDisplayName(distanceProperty);

        _selectedItemProperties.Add($"{distanceDN}: {_selectedItem.Distance}");

        //Angle
        var angleProperty = elementType.GetProperty(nameof(ElementDto.Angle));
        var angleDN = DisplayNameReader.GetDisplayName(angleProperty);

        _selectedItemProperties.Add($"{angleDN}: {_selectedItem.Angle}");

        //Width
        var widthProperty = elementType.GetProperty(nameof(ElementDto.Width));
        var widthDN = DisplayNameReader.GetDisplayName(widthProperty);

        _selectedItemProperties.Add($"{widthDN}: {_selectedItem.Width}");

        //Height
        var heightProperty = elementType.GetProperty(nameof(ElementDto.Height));
        var heightDN = DisplayNameReader.GetDisplayName(heightProperty);

        _selectedItemProperties.Add($"{heightDN}: {_selectedItem.Height}");

        //IsDefect
        var isDefectProperty = elementType.GetProperty(nameof(ElementDto.IsDefect));
        var isDefectDN = DisplayNameReader.GetDisplayName(isDefectProperty);

        if(_selectedItem.IsDefect)
            _selectedItemProperties.Add($"{isDefectDN}: да");
        else
            _selectedItemProperties.Add($"{isDefectDN}: нет");
    }

    #endregion
}