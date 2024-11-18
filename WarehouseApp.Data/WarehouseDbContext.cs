using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WarehouseApp.Data.Models;
using WarehouseApp.Data.Models.Users;

namespace WarehouseApp.Data
{
    public class WarehouseDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public WarehouseDbContext()
        {
            
        }

        public WarehouseDbContext(DbContextOptions options)
            :base(options) 
        {

        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<MessagesForWarehouseWorker> MessagesForWarehouseWorkers { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderProduct> OrderProducts { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public virtual DbSet<Request> Requests { get; set; } = null!;
        public virtual DbSet<RequestProduct> RequestProducts { get; set; } = null!;
        public virtual DbSet<Sale> Sales { get; set; } = null!;
        public virtual DbSet<SaleProduct> SaleProducts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Distributor>().ToTable("Distributors");
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Supplier>().ToTable("Suppliers");
            modelBuilder.Entity<WarehouseWorker>().ToTable("WarehouseWorkers");

            // Основната таблица за общите данни
            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");

        }
    }
}
