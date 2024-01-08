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

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasMany(e => e.Products)
                .WithOne(e => e.Category)
                .HasForeignKey(e => e.CategoryId);
            });
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Order>(e =>
            {
                e.HasOne(order => order.User).WithMany(user => user.Orders).HasForeignKey(order => order.UserId).IsRequired();
                e.HasMany(order => order.OrderDetails).WithOne(orderDetail => orderDetail.Order).HasForeignKey(orderDetail => orderDetail.Id).IsRequired();
            });

            modelBuilder.Entity<OrderDetail>()
                 .HasOne(od => od.Product)
                 .WithMany(p => p.OrderDetails)
                 .HasForeignKey(od => od.ProductID);

            modelBuilder.Entity<Product>().HasMany(od => od.OrderDetails);
            modelBuilder.Entity<Product>().HasOne(cat => cat.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId);
            modelBuilder.Entity<Category>().HasMany<Product>(p => p.Products)
                .WithOne(cat => cat.Category)
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
