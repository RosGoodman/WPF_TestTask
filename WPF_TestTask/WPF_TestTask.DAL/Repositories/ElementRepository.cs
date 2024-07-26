#nullable disable

using Microsoft.EntityFrameworkCore;
using Serilog;
using WPF_TestTask.DAL.DataContext;
using WPF_TestTask.Model.Extensioins;
using WPF_TestTask.Model.Models;

namespace WPF_TestTask.DAL.Repositories;

/// <summary>
/// Интерфейс репозитория деталей.
/// </summary>
public interface IElementRepository : IRepository<Element> { }

/// <summary>
/// Репозиторий для деталей.
/// </summary>
public class ElementRepository : IElementRepository
{
    private readonly ILogger _logger;
    private readonly IContextDB _context;

    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="logger"></param>
    public ElementRepository(ILogger logger, IContextDB context)
    {
        _logger = logger;
        _logger.Information($"Логгер встроен в {nameof(ElementRepository)}");
        _context = context;
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// <inheritdoc cref="IRepository{M}.Add"/>
    /// </summary>
    public bool Add(Element entity)
    {
        _logger?.Debug($"{nameof(ElementRepository)} >>> {nameof(Add)}");

        entity.Id = default;

        _context.Elements.Add(entity);
        if (_context.ContextSaveChanges() == 0) return false;

        _context.SetNewEntityState(entity, EntityState.Detached);
        return true;
    }

    /// <summary>
    /// <inheritdoc cref="IRepository{M}.Delete"/>
    /// </summary>
    public bool Delete(Guid id)
    {
        _logger?.Debug($"{nameof(ElementRepository)} >>> {nameof(Delete)}");

        var entityDb = _context!.Elements
            .FirstOrDefault(e => e.Id == id);

        if (entityDb is null) return false;

        _context.Elements.Remove(entityDb);

        if (_context.ContextSaveChanges() == 0)
        {
            _context.SetNewEntityState(entityDb, EntityState.Detached);
            return false;
        }
        return true;
    }

    /// <summary>
    /// <inheritdoc cref="IRepository{M}.GetAll"/>
    /// </summary>
    public List<Element> GetAll(bool asNoTracking = true)
    {
        _logger?.Debug($"{nameof(ElementRepository)} >>> {nameof(GetAll)}");

        var query = _context!.Elements.AsQueryable();

        if (asNoTracking)
            query = query.AsNoTracking();

        return query.ToList();
    }

    /// <summary>
    /// <inheritdoc cref="IRepository{M}.Update"/>
    /// </summary>
    public bool Update(Element entity)
    {
        _logger?.Debug($"{nameof(ElementRepository)} >>> {nameof(Update)}");

        _context!.Elements.Update(entity);
        bool isSaved = _context.ContextSaveChanges() != 0;

        _context.SetNewEntityState(entity, EntityState.Detached);

        return isSaved;
    }

    /// <summary>
    /// <inheritdoc cref="IRepository{M}.UpdateDetached"/>
    /// </summary>
    public bool UpdateDetached(Element entity)
    {
        _logger?.Debug($"{nameof(ElementRepository)} >>> {nameof(UpdateDetached)}");

        var entityDb = _context!.Elements.FirstOrDefault(e => e.Id == entity.Id);
        if (entityDb is null) return false;

        entityDb.CopyProps(entity);

        _context.Elements.Update(entityDb);
        bool isSaved = _context.ContextSaveChanges() != 0;

        _context.SetNewEntityState(entityDb, EntityState.Detached);
        return isSaved;
    }
}