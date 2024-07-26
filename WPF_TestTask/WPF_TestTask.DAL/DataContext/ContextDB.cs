using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WPF_TestTask.Model.Models;

namespace WPF_TestTask.DAL.DataContext;

public class ContextDB : DbContext, IContextDB
{
    public DbSet<Element> Elements { get; set; }

    public ContextDB() { }

    public ContextDB(DbContextOptions<ContextDB> options)
        : base(options) { }

    /// <summary> Сохранить изменения. Метод для использования через контейнер. </summary>
    public int ContextSaveChanges() => SaveChanges();

    /// <summary> Назначить состояние объекта "модифицируемый". </summary>
    /// <param name="entity"> Объект для изменения состояния. </param>
    public void SetNewEntityState(object entity, EntityState newState) => Entry(entity).State = newState;

    /// <summary>
    /// Получить состояние объекта.
    /// </summary>
    /// <param name="entity"> Экземпляр объекта. </param>
    /// <returns> Текущее состояние экземпляра объекта. </returns>
    public EntityState GetEntityState(object entity) => Entry(entity).State;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var _connString = System.Configuration.ConfigurationManager.ConnectionStrings["SqliteConnString"]?.ConnectionString;

        //optionsBuilder
        //    .EnableSensitiveDataLogging()
        //    .LogTo(message => Debug.WriteLine(message), Microsoft.Extensions.Logging.LogLevel.Debug);

        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite(_connString, option =>
            {
                option.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}