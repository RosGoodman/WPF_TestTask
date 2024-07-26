using Logger;
using Ninject;
using Serilog;
using System.Windows;
using WPF_TestTask.ViewModel.IoC;
using WPF_TestTask.ViewModel.ViewModels.Windows;

namespace WPF_TestTask;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private const string _loggerPath = "\\logs\\log_.txt";
    private static MainWindowVM _mainWindowVM;

    /// <summary> Кастомный стартап. </summary>
    /// <param name="e"></param>
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        //проверка попытки запуска дубликата
        if (!InstanceCheck())
            CloseApp();

        LogStarter.CreateLogger(string.Empty, _loggerPath);

        Log.Logger.Information($"Логгер встроен в программу");

        ViewModelsIoC.Setup();

        //AppControlInit.Initialize();

        ////создание дефолтных данных в БД (при необходимости)
        //var defaultEntityCreator = ViewModelsIoC.Kernel.Get<IDefaultEntityCreator>();
        //defaultEntityCreator.CreateDefaultEntities();


        Current.MainWindow = new MainWindow();
        _mainWindowVM = ViewModelsIoC.Kernel.Get<MainWindowVM>();
        Current.MainWindow.DataContext = _mainWindowVM;

        //подписка на закрытие программы при обновлении
        if (_mainWindowVM is not null)
            _mainWindowVM.CloseApp += CloseApp;

        Current.MainWindow.Closed += CloseApp;
        Current.MainWindow.Show();
    }

    #region CloseApp

    private static void CloseApp()
    {
        Application.Current.Shutdown();
    }

    private static void CloseApp(object? sender, EventArgs e)
    {
        Application.Current.Shutdown();
    }

    #endregion

    // держим в переменной, чтобы сохранить владение им до конца пробега программы
    static Mutex? InstanceCheckMutex;

    /// <summary> Проверить существование запущеного экземпляра программы. </summary>
    /// <returns> True - существует. </returns>
    static bool InstanceCheck()
    {
        var mutex = new Mutex(true, "Logistic.exe", out bool isNew);
        if (isNew)
            InstanceCheckMutex = mutex;
        else
            mutex.Dispose(); // отпустить mutex сразу
        return isNew;
    }
}

