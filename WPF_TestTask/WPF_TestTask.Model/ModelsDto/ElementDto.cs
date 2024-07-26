#nullable disable

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
    /// Дистанция (м).
    /// </summary>
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
    /// Состояние детали (наличие дефектов).
    /// </summary>
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
