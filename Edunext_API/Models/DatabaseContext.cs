using Edunext_Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Edunext_API.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
<<<<<<< HEAD
            modelBuilder.Entity<Product>().HasOne<Category>(cat => cat.Categories)
                 .WithMany(p => p.Products)
                 .HasForeignKey(p => p.CategoryId);
=======
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(e =>
            {
                e.HasOne(order => order.User).WithMany(user => user.Orders).HasForeignKey(order => order.UserId).IsRequired();
                e.HasMany(order => order.OrderDetails).WithOne(orderDetail => orderDetail.Order).HasForeignKey(orderDetail => orderDetail.OrderId).IsRequired();
            });
>>>>>>> 27a8aaefc0ad987a2696f292ef1f594d359aa473
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
