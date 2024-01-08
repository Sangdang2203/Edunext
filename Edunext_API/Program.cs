using Edunext_API.Models;
using Edunext_API.Repositories.GenericRepository;
using Edunext_API.Repositories.OrderDetailRepository;
using Edunext_API.Repositories.OrderRepository;
using Edunext_API.Repositories.UnitOfWork;
using Edunext_API.Services;
using Microsoft.EntityFrameworkCore;

namespace Edunext_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("connectDB"));
            });

            // Repository
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IShoppingCartUnitOfWork, ShoppingCartUnitOfWork>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<OrderDetailRepository>();

            // Service
            builder.Services.AddScoped<OrderService>();
            builder.Services.AddScoped<ShoppingCartService>();
            builder.Services.AddScoped<UserServices>();
            builder.Services.AddScoped<OrderDetailService>();
            
            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.UseSession();

            app.MapControllers();

            app.Run();
        }
    }
}