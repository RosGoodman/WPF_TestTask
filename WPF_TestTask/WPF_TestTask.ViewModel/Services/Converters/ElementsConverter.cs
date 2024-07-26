using WPF_TestTask.Model.Models;
using WPF_TestTask.Model.ModelsDto;

namespace WPF_TestTask.ViewModel.Services.Converters;

/// <summary>
/// Конвертер для экземпляров <see cref="Element"/> и <see cref="ElementDto"/>
/// </summary>
internal static class ElementsConverter
{
    internal static List<ElementDto> Convert(string[,] values)
    {
        var elements = new List<ElementDto>();

        if (values is null || values.Length == 0)
            return elements;

        for (int i = 0; i < values.GetLength(0); i++)
        {
            elements.Add(new ElementDto()
            {
                Name        = values[i, 0],
                Distance    = GetFloatValue(values[i, 1]),
                Angle       = GetFloatValue(values[i, 2]),
                Width       = GetFloatValue(values[i, 3]),
                Height      = GetFloatValue(values[i, 4]),
                IsDefect    = values[i, 5] == "yes",
            });
        }

        return elements;
    }

    private static float GetFloatValue(string value)
    {
        if(float.TryParse(value, out var floatValue))
            return floatValue;

        return 0f;
    }
}
