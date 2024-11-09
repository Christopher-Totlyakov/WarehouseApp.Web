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

        DbSet<Category> Categories { get; set; } = null!;
        DbSet<Message> Messages { get; set; } = null!;
        DbSet<MessagesForWarehouseWorker> MessagesForWarehouseWorkers { get; set; } = null!;
        DbSet<Order> Orders { get; set; } = null!;
        DbSet<OrderProduct> OrderProducts { get; set; } = null!;
        DbSet<Product> Products { get; set; } = null!;
        DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        DbSet<Request> Requests { get; set; } = null!;
        DbSet<RequestProduct> RequestProducts { get; set; } = null!;
        DbSet<Sale> Sales { get; set; } = null!;
        DbSet<SaleProduct> SaleProducts { get; set; } = null!;
        DbSet<SupplierOrder> SupplierOrders { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
