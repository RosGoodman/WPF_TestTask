using WPF_TestTask.Model.Models;

namespace WPF_TestTask.Model.Extensioins;

public static class ElementExtension
{
    public static void CopyProps(this Element entity, Element sender)
    {
        if (sender is null)
        {
            entity = null!;
            return;
        }

        entity ??= new();
        entity.Name = sender.Name;
        entity.Distance = sender.Distance;
        entity.Angle = sender.Angle;
        entity.Width = sender.Width;
        entity.Height = sender.Height;
        entity.IsDefect = sender.IsDefect;
    }

    public static bool IsValid(this Element entity)
    {
        if (entity is null || entity.Name == string.Empty)
            return false;

        return true;
    }
}
