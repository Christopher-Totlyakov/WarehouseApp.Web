using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WarehouseApp.Data.Models;

namespace WarehouseApp.Data
{
    public class WarehouseDbContext : DbContext
    {
        public WarehouseDbContext()
        {
            
        }

        public WarehouseDbContext(DbContextOptions options)
            :base(options) 
        {

        }

        DbSet<Movie> Movies { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
