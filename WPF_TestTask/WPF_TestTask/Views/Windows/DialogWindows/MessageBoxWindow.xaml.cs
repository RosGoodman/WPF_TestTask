using MessageService;
using System.Windows;

namespace WPF_TestTask.Views.Windows.DialogWindows;

/// <summary>
/// Логика взаимодействия для MessageBoxWindow.xaml
/// </summary>
public partial class MessageBoxWindow : Window
{
    private readonly MessageBoxEventArgs _e;
    public string MessageText { get; private set; }
    public string Caption { get; private set; }

    public MessageBoxWindow(MessageBoxEventArgs e)
    {
        InitializeComponent();
        WindowStartupLocation = WindowStartupLocation.CenterScreen;

        _e = e;
        this.MessageText = e.Message;
        this.Caption = e.Caption;

        Message.Text = e.Message;
        Title = e.Caption;
        ResponseMessageTB.Text = e.DefaultResultMessage;

        ShowButtons();
        ShowResponseTextBox();
    }


    #region ButtonsClick

    /// <summary>
    /// Обработка нажатия на кнопку "Ok"/
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OkButton_Click(object sender, RoutedEventArgs e)
    {
        _e.ResultAction(MessageService.Enums.MessageBoxResultEnum.OK, ResponseMessageTB.Text);
        this.Close();
    }

    /// <summary>
    /// Обработка нажатия на кнопку "Yes"/
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void YesButton_Click(object sender, RoutedEventArgs e)
    {
        _e.ResultAction(MessageService.Enums.MessageBoxResultEnum.Yes, ResponseMessageTB.Text);
        this.Close();
    }

    /// <summary>
    /// Обработка нажатия на кнопку "No"/
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NoButton_Click(object sender, RoutedEventArgs e)
    {
        _e.ResultAction(MessageService.Enums.MessageBoxResultEnum.No, ResponseMessageTB.Text);
        this.Close();
    }

    /// <summary>
    /// Обработка нажатия на кнопку "Cancel"/
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        _e.ResultAction(MessageService.Enums.MessageBoxResultEnum.Cancel, ResponseMessageTB.Text);
        this.Close();
    }

    #endregion

    /// <summary>
    /// Отобразить TextBox для ввода ответа.
    /// </summary>
    private void ShowResponseTextBox()
    {
        if (_e.NeedResponseStr)
            ResponseMessageTB.Visibility = Visibility.Visible;
    }

    /// <summary>
    /// Отобразиты выбранные кнопки.
    /// </summary>
    private void ShowButtons()
    {
        switch (_e.Button)
        {
            case MessageService.Enums.MessageBoxButtonEnum.OK:
                ButtonOk.Visibility = Visibility.Visible;
                ButtonOk.Focus();
                break;
            case MessageService.Enums.MessageBoxButtonEnum.YesNo:
                ButtonYes.Visibility = Visibility.Visible;
                ButtonNo.Visibility = Visibility.Visible;
                break;
            case MessageService.Enums.MessageBoxButtonEnum.OKCancel:
                ButtonOk.Visibility = Visibility.Visible;
                ButtonCancel.Visibility = Visibility.Visible;
                break;
            case MessageService.Enums.MessageBoxButtonEnum.YesNoCancel:
                ButtonYes.Visibility = Visibility.Visible;
                ButtonNo.Visibility = Visibility.Visible;
                ButtonCancel.Visibility = Visibility.Visible;
                break;
        }
    }
}
