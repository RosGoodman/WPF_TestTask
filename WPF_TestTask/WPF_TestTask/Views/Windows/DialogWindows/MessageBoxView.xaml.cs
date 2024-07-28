using MessageService;
using Ninject;
using System.Windows.Controls;
using WPF_TestTask.ViewModel.IoC;
using WPF_TestTask.ViewModel.ViewModels.Windows.DialogWindows;

namespace WPF_TestTask.Views.Windows.DialogWindows;

/// <summary>
/// Логика взаимодействия для MessageBoxView.xaml
/// </summary>
public partial class MessageBoxView : UserControl
{
    public MessageBoxView()
    {
        InitializeComponent();
        this.DataContext = ViewModelsIoC.Kernel.Get<IMessageBoxVM>();

        if (this.DataContext is null) return;
        (DataContext as IMessageBoxVM)!.MessageBoxRequest += MessageBoxView_MessageBoxRequest;
    }

    private void MessageBoxView_MessageBoxRequest(object sender, MessageBoxEventArgs e)
    {
        var messageBox = new MessageBoxWindow(e);
        messageBox.ShowDialog();
    }
}
