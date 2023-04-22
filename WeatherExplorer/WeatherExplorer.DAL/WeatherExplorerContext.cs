using Microsoft.EntityFrameworkCore;
using WeatherExplorer.DAL.Models;

namespace WeatherExplorer.DAL
{
    public class WeatherExplorerContext : DbContext
    {
        /*
         * TODO:
         * допилить конструктор, 
         * инициализацию моделей, 
         * прокинуть индексы на таблицы
         * закинуть defaultValue для таблицы с направлением ветра, погодой
         * 
         * сделать IRepository
         */
        public WeatherExplorerContext(DbContextOptions<WeatherExplorerContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<City>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Weather>()
                .HasIndex(w => new { w.CityId, w.Date, w.Time })
                .IsUnique();

            modelBuilder.Entity<WeatherCondition>();
        }
        public DbSet<City> Cities { get; set; }
        public DbSet<Weather> Wheathers { get; set; }
        public DbSet<WeatherCondition> WeatherConditions { get; set; }

    }
}
