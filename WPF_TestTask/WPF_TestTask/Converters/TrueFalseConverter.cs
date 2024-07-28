using System.Globalization;
using System;
using System.Windows.Data;

namespace WPF_TestTask.Converters;

public class TrueFalseConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool val = (bool)value;

        if (val)
            return "да";
        else
            return "нет";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string val = value.ToString()!.ToLower();

        if (val == "да")
            return true;
        else
            return false;
    }
}
