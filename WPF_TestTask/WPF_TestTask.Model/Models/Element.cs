#nullable disable

namespace WPF_TestTask.Model.Models;

/// <summary>
/// Модель детали, описываемой в программе.
/// </summary>
/// <remarks>
/// На практике параметры объекта вроде ВГХ и статуса дефектовки, с большой вероятностью пришлось бы вынести
/// в отдельные объекты т.к. сегодня необходимо в программе описывать прямоуголььные объекты с 2-я состояниями,
/// а завтра еще и овалы с 5-ю состояниями.
/// </remarks>
public class Element : BaseModel
{
    /// <summary>
    /// Наименование.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Дистанция (м).
    /// </summary>
    public float Distance { get; set; }

    /// <summary>
    /// Угол (ч).
    /// </summary>
    public float Angle { get; set; }

    /// <summary>
    /// Ширина детали.
    /// </summary>
    public float Width { get; set; }

    /// <summary>
    /// Высота детали.
    /// </summary>
    public float Height { get; set; }

    /// <summary>
    /// Состояние детали (наличие дефектов).
    /// </summary>
    public bool IsDefect { get; set; }
}
