#nullable disable

using Serilog;
using System.ComponentModel;

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

    #endregion

    /////////////////////////////////////////////////////////////////////////////////

    #region CommandProperties


    #endregion

    /////////////////////////////////////////////////////////////////////////////////

    /// <summary> Ctor. </summary>
    public MainWindowVM(ILogger logger)
    {
        _logger = logger;
        _logger.Information($"Логгер встроен в {nameof(MainWindowVM)}");
    }

    /////////////////////////////////////////////////////////////////////////////////

    #region CommandMethods


    #endregion

    /////////////////////////////////////////////////////////////////////////////////

    #region ViewModelMethods


    #endregion
}