namespace WPF_TestTask.Model.ModelsDto;

public class BaseModelDto
{
    private bool _isChanged = false;
    private bool _isDeleted = false;

    /// <summary>
    /// Порядковый индекс элемента.
    /// </summary>
    /// <remarks>
    /// Используется для отображения в списке.
    /// </remarks>
    public int Index { get; set; }

    public bool IsChanged
    {
        get => _isChanged;
        set
        {
            _isChanged = value;
        }
    }

    public bool IsDeleted
    {
        get => _isDeleted;
        set
        {
            _isDeleted = value;
        }
    }
}
