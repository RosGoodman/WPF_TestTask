#nullable disable

using System.ComponentModel;

namespace WPF_TestTask.Model.ModelsDto;

/// <summary>
/// Модель отображаемого экземпляра.
/// </summary>
public class ElementDto : BaseModelDto
{
    private Guid _id;
    private string _name = string.Empty;
    private float _distance;
    private float _angle;
    private float _width;
    private float _height;
    private bool _isDefect;


    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id
    {
        get => _id;
        set
        {
            _id = value;
        }
    }

    /// <summary>
    /// Наименование.
    /// </summary>
    [DisplayName("Наименование")]
    public string Name
    {
        get => _name;
        set
        {
            if(_name == value) return;
            _name = value;
            IsChanged = true;
        }
    }

    /// <summary>
    /// Расстояние (м).
    /// </summary>

    [DisplayName("Расстояние")]
    public float Distance
    {
        get => _distance;
        set
        {
            if (_distance == value) return;
            _distance = value;
            IsChanged = true;
        }
    }

    /// <summary>
    /// Угол (ч).
    /// </summary>

    [DisplayName("Угол")]
    public float Angle
    {
        get => _angle;
        set
        {
            if (_angle == value) return;
            _angle = value;
            IsChanged = true;
        }
    }

    /// <summary>
    /// Ширина детали.
    /// </summary>

    [DisplayName("Ширина")]
    public float Width
    {
        get => _width;
        set
        {
            if (_width == value) return;
            _width = value;
            IsChanged = true;
        }
    }

    /// <summary>
    /// Высота детали.
    /// </summary>

    [DisplayName("Высота")]
    public float Height
    {
        get => _height;
        set
        {
            if (_height == value) return;
            _height = value;
            IsChanged = true;
        }
    }

    /// <summary>
    /// Является дефектом.
    /// </summary>

    [DisplayName("Является дефектом")]
    public bool IsDefect
    {
        get => _isDefect;
        set
        {
            if (_isDefect == value) return;
            _isDefect = value;
            IsChanged = true;
        }
    }
}
