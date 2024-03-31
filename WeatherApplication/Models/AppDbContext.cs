//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace WeatherApplication.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Weather> Weathers { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Weather>().Property(b => b.Data).IsRequired();
            modelBuilder.Entity<Weather>().Property(b => b.Time).IsRequired();
            modelBuilder.Entity<Weather>().Property(b => b.T).IsRequired();
        }
    }
}
