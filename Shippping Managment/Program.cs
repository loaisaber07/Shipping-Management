using Data_Access_Layer;
using Data_Access_Layer.Entity;
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
            builder.Services.AddIdentity<Seller ,IdentityRole>().
                AddEntityFrameworkStores<ShippingDataBase>();
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
                 var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
