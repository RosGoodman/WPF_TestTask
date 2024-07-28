#nullable disable

namespace MessageService;

/// <summary>
/// Модель сущности для работы со списком.
/// </summary>
public class Entity
{
    public string Value { get; private set; }

    public Entity(string value)
    {
        Value = value;
    }

    public Entity() { }
}
