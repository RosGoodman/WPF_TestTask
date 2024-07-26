#nullable disable

namespace WPF_TestTask.DAL.Repositories;

/// <summary>
/// Основной интерфейс репозиториев.
/// </summary>
/// <typeparam name="M"> Экземпляр обрабатываемой модели. </typeparam>
public interface IRepository<M>
{
    /// <summary>
    /// Добавить экземпляр модели в БД.
    /// </summary>
    /// <param name="entity"> Добавляемый экземпляр. </param>
    public bool Add(M entity);

    /// <summary>
    /// Обновить экземпляр модели в БД.
    /// </summary>
    /// <param name="entity"> Обновляемый экземпляр. </param>
    /// <returns> Результат выполнения операции. </returns>
    public bool Update(M entity);

    /// <summary>
    /// Обновить неотслеживаемый EF экземпляр модели.
    /// </summary>
    /// <param name="entity"> Обновляемый экземпляр модели. </param>
    public bool UpdateDetached(M entity);

    /// <summary>
    /// Удалить экземпляр из БД по Id.
    /// </summary>
    /// <param name="id">Id удаляемого элемента.</param>
    /// <returns> Результат выполнения операции. </returns>
    public bool Delete(Guid id);

    /// <summary>
    /// Получить список всех экземпляров.
    /// </summary>
    /// <param name="asNoTracking"> Отслеживать экземпляры EF. </param>
    /// <returns> Список экземпляров моделей. </returns>
    public List<M> GetAll(bool asNoTracking = true);
}
