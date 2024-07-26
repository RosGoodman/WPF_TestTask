using Microsoft.EntityFrameworkCore;
using WPF_TestTask.Model.Models;

namespace WPF_TestTask.DAL.DataContext;

/// <summary>
/// Контекст БД.
/// </summary>
public interface IContextDB
{
    DbSet<Element> Elements { get; set; }


    /// <summary> Сохранить изменения. Метод для использования через контейнер. </summary>
    int ContextSaveChanges();

    /// <summary> Назначить состояние объекта "модифицируемый". </summary>
    /// <param name="entity"> Объект для изменения состояния. </param>
    void SetNewEntityState(object entity, EntityState newState);

    /// <summary>
    /// Получить состояние объекта.
    /// </summary>
    /// <param name="entity"> Экземпляр объекта. </param>
    /// <returns> Текущее состояние экземпляра объекта. </returns>
    EntityState GetEntityState(object entity);
}