using Microsoft.EntityFrameworkCore;
using WeatherApplication.Models;

namespace WeatherApplication.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Weather> Weathers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Weather>().Property(b => b.Data).IsRequired();
            modelBuilder.Entity<Weather>().Property(b => b.Time).IsRequired();
            modelBuilder.Entity<Weather>().Property(b => b.T).IsRequired();
        }
    }
}
