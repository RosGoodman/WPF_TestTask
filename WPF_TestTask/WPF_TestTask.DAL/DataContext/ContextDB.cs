using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;
using WPF_TestTask.Model.Models;

namespace WPF_TestTask.DAL.DataContext;

public class ContextDB : DbContext, IContextDB
{
    public DbSet<Element> Elements { get; set; }

    public ContextDB() { }

    public ContextDB(DbContextOptions<ContextDB> options)
        : base(options) { }

    /// <inheritdoc/>
    public int ContextSaveChanges() => SaveChanges();

    /// <inheritdoc/>
    public void SetNewEntityState(object entity, EntityState newState) => Entry(entity).State = newState;

    /// <inheritdoc/>
    public EntityState GetEntityState(object entity) => Entry(entity).State;

    /// <inheritdoc/>
    public IDbContextTransaction ContextBeginTransaction() => Database.BeginTransaction();

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