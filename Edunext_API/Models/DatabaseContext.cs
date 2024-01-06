using Edunext_Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Edunext_API.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }
<<<<<<< HEAD
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
=======
>>>>>>> ac65bf5eecdb2ce81e28635fcb992174033ade82

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(e =>
            {
                e.HasOne(order => order.User).WithMany(user => user.Orders).HasForeignKey(order => order.UserId).IsRequired();
                e.HasMany(order => order.OrderDetails).WithOne(orderDetail => orderDetail.Order).HasForeignKey(orderDetail => orderDetail.Id).IsRequired();
            });
<<<<<<< HEAD

            modelBuilder.Entity<OrderDetail>()
                 .HasOne(od => od.Product)
                 .WithMany(p => p.OrderDetails)
                 .HasForeignKey(od => od.ProductID);

        }
=======
        }

        public DbSet<Order> Orders { get; set; }
>>>>>>> ac65bf5eecdb2ce81e28635fcb992174033ade82
    }
}
