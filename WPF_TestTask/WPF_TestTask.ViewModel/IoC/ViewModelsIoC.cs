using DataReaderService;
using Ninject;
using Serilog;
using WPF_TestTask.DAL.DataContext;
using WPF_TestTask.DAL.Repositories;
using WPF_TestTask.ViewModel.ViewModels.Windows;

namespace WPF_TestTask.ViewModel.IoC;

public class ViewModelsIoC
{
    public static IKernel Kernel { get; private set; } = new StandardKernel();

    /// <summary> Настройкка IoC контейнера, привязка информации. </summary>
    public static void Setup()
    {
        Kernel.Bind<ILogger>().ToMethod(p => Log.Logger);

        BindContext();
        BindRepositories();
        BindViewModels();
        BindServices();
    }

    private static void BindServices()
    {
        Kernel.Bind<IDataReader>().To<DataReader>().InTransientScope();
    }

    private static void BindContext()
    {
        Kernel.Bind<IContextDB>().To<ContextDB>().InTransientScope();
    }

    private static void BindRepositories()
    {
        Kernel.Bind<IElementRepository>().To<ElementRepository>().InTransientScope();
    }

    private static void BindViewModels()
    {
        Kernel.Bind<MainWindowVM>().ToSelf().InSingletonScope();
    }
}
