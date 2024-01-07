using Edunext_Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Edunext_API.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(e =>
            {
                e.HasOne(order => order.User).WithMany(user => user.Orders).HasForeignKey(order => order.UserId).IsRequired();
                e.HasMany(order => order.OrderDetails).WithOne(orderDetail => orderDetail.Order).HasForeignKey(orderDetail => orderDetail.Id).IsRequired();
            });
            modelBuilder.Entity<OrderDetail>()
                 .HasOne(od => od.Product)
                 .WithMany(p => p.OrderDetails)
                 .HasForeignKey(od => od.ProductID);
            modelBuilder.Entity<Product>().HasMany<OrderDetail>(od => od.OrderDetails)
            modelBuilder.Entity<Product>().HasOne<Category>(cat => cat.Categories)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId);
            modelBuilder.Entity<Category>().HasMany<Product>(p => p.Products)
                .WithOne(cat => cat.Categories)
                .HasForeignKey(p => p.CategoryId);
            modelBuilder.Entity<User>().HasMany<Order>(o => o.Orders)
                .WithOne(u => u.User)
                .HasForeignKey(o => o.UserId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
