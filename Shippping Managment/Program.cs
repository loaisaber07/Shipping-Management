using Business_Layer.Services;
using Data_Access_Layer;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Repositry;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Shippping_Managment
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
            builder.Services.AddDbContext<ShippingDataBase>(option => {
                option.UseSqlServer(builder.Configuration.GetConnectionString("default"));
            }); 
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ShippingDataBase>();
            builder.Services.AddScoped<ICity,CityRepository>();

            builder.Services.AddScoped<IGovern,GovernRepository>();
            builder.Services.AddScoped<IBranch, BranchRepository>();
            builder.Services.AddScoped<IFieldJob, FieldJobRepository>();
            builder.Services.AddScoped<IFieldPrivilege, FiledPrivilegeRepository>();
            builder.Services.AddScoped<IPrivilege, PrivilegeRepository>();
            builder.Services.AddScoped<IUser, User>();
            builder.Services.AddScoped<IProduct, ProductRepository>();
            builder.Services.AddScoped<ITypeOfPayment, TypeOfPaymentRepository>();
            builder.Services.AddScoped<ITypeOfCharge, TypeOfChargeRepository>();
            builder.Services.AddScoped<IOrderStatus, OrderStatusRepository>();
            builder.Services.AddScoped<IOrder,OrderRepository>();
            builder.Services.AddScoped<ISpecialCharge, SpecialChargeRepo>();
            builder.Services.AddScoped<ISeller, SellerRepository>();
            builder.Services.AddScoped<IWeight,WeightRepository>();
            builder.Services.AddScoped<ITypeOfReceipt, TypeOfReceiptRepository>();
            builder.Services.AddScoped<InvoiceService>();
            builder.Services.AddAuthentication(option => option.DefaultAuthenticateScheme = "mySchema")
            .AddJwtBearer("mySchema", op =>
            {
                var JwtSetting = builder.Configuration.GetSection("JwtSetting");
                string? key = JwtSetting["SecretKey"];
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                op.TokenValidationParameters = new TokenValidationParameters() { 
                IssuerSigningKey=secretKey , 
                ValidateIssuer=false , 
                ValidateAudience=false
                
                }; 
            });

            builder.Services.AddCors(option => {
                option.AddPolicy("Allow", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();

                });
            
            });
                 var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("Allow");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
