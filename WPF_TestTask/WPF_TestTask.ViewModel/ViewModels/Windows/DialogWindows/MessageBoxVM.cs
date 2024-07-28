using MessageService;
using MessageService.Enums;
using Serilog;

namespace WPF_TestTask.ViewModel.ViewModels.Windows.DialogWindows;

/// <summary>  </summary>
public class MessageBoxVM : IMessageBoxVM
{
    private readonly ILogger _logger;
    public event Action<object, MessageBoxEventArgs>? MessageBoxRequest;

    #region Properties

    /// <summary>
    /// Результат ввода текста пользователем.
    /// </summary>
    public string ResponseMessage { get; private set; } = string.Empty;

    /// <summary>
    /// Нажатая кнопка.
    /// </summary>
    public MessageBoxResultEnum ButtonResult { get; private set; }

    #endregion

    /////////////////////////////////////////////////////////////////////////////////

    #region CommandProperties


    #endregion

    /////////////////////////////////////////////////////////////////////////////////

    /// <summary> Ctor. </summary>
    public MessageBoxVM(ILogger logger)
    {
        _logger = logger;
        _logger.Information($"Логгер встроен в {nameof(MessageBoxVM)}.");
    }

    /////////////////////////////////////////////////////////////////////////////////

    #region CommandMethods



    #endregion

    /////////////////////////////////////////////////////////////////////////////////

    #region ViewModelMethods

    protected void MessageBox_Show(
        Action<MessageBoxResultEnum, string> resultAction,
        string message,
        bool needResponseStr = false,
        string caption = "",
        string defaultResultMessage = "",
        MessageBoxButtonEnum button = MessageBoxButtonEnum.OK)
    {
        MessageBoxRequest?.Invoke(this, new MessageBoxEventArgs(resultAction, message, needResponseStr, caption, defaultResultMessage, button));
    }

    public void AskTheQuestion(
        string message,
        string caption = "",
        bool needResponseStr = false,
        string defaultResultMessage = "",
        MessageBoxButtonEnum button = MessageBoxButtonEnum.OK)
    {
        ResponseMessage = string.Empty;
        ButtonResult = MessageBoxResultEnum.OK;
        MessageBox_Show(ProcessTheAnswer, message, needResponseStr, caption, defaultResultMessage, button);
    }

    public void AskTheQuestion(
        string message,
        string caption = "",
        MessageBoxButtonEnum button = MessageBoxButtonEnum.OK)
    {
        ResponseMessage = string.Empty;
        ButtonResult = MessageBoxResultEnum.OK;
        MessageBox_Show(ProcessTheAnswer, message, false, caption, string.Empty, button);
    }

    public void ProcessTheAnswer(MessageBoxResultEnum buttonResult, string response)
    {
        ResponseMessage = response;
        ButtonResult = buttonResult;
    }

    #endregion
}