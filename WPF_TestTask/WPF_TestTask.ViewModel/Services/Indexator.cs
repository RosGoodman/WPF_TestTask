using System.Collections.ObjectModel;
using WPF_TestTask.Model.ModelsDto;

namespace WPF_TestTask.ViewModel.Services;

internal static class Indexator
{
    internal static List<T> SetIndexes<T>(List<T> entities) 
        where T : BaseModelDto
    {
        if (entities is null || entities.Count == 0)
            return entities!;

        for (int i = 0; i < entities.Count; i++)
            entities[i].Index = i + 1;

        return entities;
    }

    internal static ObservableCollection<T> SetIndexes<T>(ObservableCollection<T> entities)
         where T : BaseModelDto
    {
        if (entities is null || entities.Count == 0)
            return entities!;

        for (int i = 0; i < entities.Count; i++)
            entities[i].Index = i + 1;

        return entities;
    }
}
