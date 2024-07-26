using AutoMapper;
using System.Collections.ObjectModel;

namespace WPF_TestTask.ViewModel.Services.IntermediateLogics.MapperService;

internal static class StandartMapper
{
    internal static K Map<T, K>(T entity) where T : class
    {
        var cfg = new MapperConfiguration(cfg => cfg.CreateMap<T, K>());
        var mapper = new Mapper(cfg);
        return mapper.Map<K>(entity);
    }

    internal static List<K> Map<T, K>(List<T> entities) where T : class
    {
        var cfg = new MapperConfiguration(cfg => cfg.CreateMap<T, K>());
        var mapper = new Mapper(cfg);
        return mapper.Map<List<K>>(entities);
    }

    internal static ObservableCollection<K> Map<T, K>(ObservableCollection<T> entities) where T : class
    {
        var cfg = new MapperConfiguration(cfg => cfg.CreateMap<T, K>());
        var mapper = new Mapper(cfg);
        return mapper.Map<ObservableCollection<K>>(entities);
    }
}
