using MessageService.Enums;

namespace MessageService;

public class MessageBoxEventArgs : System.EventArgs
{
    /// <summary>
    /// Действие при получении результата.
    /// </summary>
    public Action<MessageBoxResultEnum, string> ResultAction { get; set; }
    public string Message { get; set; }
    public string Caption { get; set; }
    public string DefaultResultMessage { get; set; }
    public bool NeedResponseStr { get; set; }
    public MessageBoxButtonEnum Button { get; set; }

    public MessageBoxEventArgs(
        Action<MessageBoxResultEnum, string> resultAction,
        string message,
        bool needResponseStr = false,
        string caption = "",
        string defaultResultMessage = "",
        MessageBoxButtonEnum button = MessageBoxButtonEnum.OK)
    {
        ResultAction = resultAction;
        Message = message;
        DefaultResultMessage = defaultResultMessage;
        Caption = caption;
        Button = button;
        NeedResponseStr = needResponseStr;
    }
}
