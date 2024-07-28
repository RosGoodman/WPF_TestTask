using MessageService;
using MessageService.Enums;

namespace WPF_TestTask.ViewModel.ViewModels.Windows.DialogWindows;

public interface IMessageBoxVM
{
    MessageBoxResultEnum ButtonResult { get; }
    string ResponseMessage { get; }

    event Action<object, MessageBoxEventArgs>? MessageBoxRequest;

    void AskTheQuestion(
        string message,
        string caption = "",
        bool needResponseStr = false,
        string defaultResultMessage = "",
        MessageBoxButtonEnum button = MessageBoxButtonEnum.OK);

    public void AskTheQuestion(
        string message,
        string caption = "",
        MessageBoxButtonEnum button = MessageBoxButtonEnum.OK);

    void ProcessTheAnswer(
        MessageBoxResultEnum buttonResult,
        string response);
}