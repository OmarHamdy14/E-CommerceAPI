using ECommerceAPI.Data.Base;
using ECommerceAPI.Data.Services.Implementation;
using ECommerceAPI.Data.Services.Interface;
using ECommerceAPI.Helpers;
using ECommerceAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ECommerceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            
            builder.Services.AddScoped<IEntityBaseRepository<Order>, EntityBaseRepository<Order>>();
            builder.Services.AddScoped<IEntityBaseRepository<CartItem>, EntityBaseRepository<CartItem>>();
            builder.Services.AddScoped<IEntityBaseRepository<Category>, EntityBaseRepository<Category>>();
            builder.Services.AddScoped<IEntityBaseRepository<Notification>, EntityBaseRepository<Notification>>();
            builder.Services.AddScoped<IEntityBaseRepository<Product>, EntityBaseRepository<Product>>();
            builder.Services.AddScoped<IEntityBaseRepository<ProductImage>, EntityBaseRepository<ProductImage>>();
            builder.Services.AddScoped<IEntityBaseRepository<Shipment>, EntityBaseRepository<Shipment>>();
            builder.Services.AddScoped<IEntityBaseRepository<Payment>, EntityBaseRepository<Payment>>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IProductImageService, ProductImageService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IShipmentService, ShipmentService>();

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

            builder.Services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(op =>
            {
                op.RequireHttpsMetadata = true;
                op.SaveToken = true;
                op.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"]))
                };

            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseStaticFiles();
            
            app.UseHttpsRedirection();
            
            app.UseAuthentication();
            
            app.UseAuthorization();
            
            app.MapControllers();

            app.Run();
        }
    }
}