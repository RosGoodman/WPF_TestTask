using System.ComponentModel;
using System.Reflection;

namespace WPF_TestTask.ViewModel.Services;

internal static class DisplayNameReader
{
    /// <summary>
    /// Получить атрибут DisplayName для переданного св-ва.
    /// </summary>
    /// <param name="nameProperty"> Свойство. </param>
    /// <returns> DisplayName. </returns>
    internal static string GetDisplayName(PropertyInfo nameProperty)
    {
        if (Attribute.IsDefined(nameProperty, typeof(DisplayNameAttribute)))
        {
            var displayNameAttribute = (DisplayNameAttribute)Attribute.GetCustomAttribute(nameProperty, typeof(DisplayNameAttribute));
            return displayNameAttribute.DisplayName;
        }

        return string.Empty;
    }
}
