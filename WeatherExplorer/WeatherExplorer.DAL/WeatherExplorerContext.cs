using Microsoft.EntityFrameworkCore;
using WeatherExplorer.DAL.Models;

namespace WeatherExplorer.DAL
{
    public class WeatherExplorerContext : DbContext
    {
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

            modelBuilder.Entity<WeatherCondition>()
                .HasData(new WeatherCondition { Name = "штиль" });
            modelBuilder.Entity<WeatherCondition>()
                .HasIndex(wc => wc.Name)
                .IsUnique();
        }
        public DbSet<City> Cities { get; set; }
        public DbSet<Weather> Weathers { get; set; }
        public DbSet<WeatherCondition> WeatherConditions { get; set; }

    }
}
