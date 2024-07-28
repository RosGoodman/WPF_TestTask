using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPF_TestTask.Converters;

public class ScaleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter is null || !double.TryParse(parameter.ToString(), out double param))
            return value;
        return (double)value * param;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }
}
