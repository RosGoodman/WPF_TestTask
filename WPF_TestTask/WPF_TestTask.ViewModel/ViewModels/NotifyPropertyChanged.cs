using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF_TestTask.ViewModel.ViewModels;

public class NotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    internal void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
