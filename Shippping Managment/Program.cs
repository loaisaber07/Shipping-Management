using Business_Layer.Services;
using Data_Access_Layer;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Repositry;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
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
            var jwtSetting = builder.Configuration.GetSection("JwtSetting");
            var keyBase64 = jwtSetting["SecretKey"];
            var key = Convert.FromBase64String(keyBase64);
            var secretKey = new SymmetricSecurityKey(key);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = secretKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RoleClaimType = ClaimTypes.Role,
                    ClockSkew = TimeSpan.FromMinutes(5),
                    ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha512 }
                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Employee", policy => policy.RequireClaim(ClaimTypes.Role, "Employee"));
            });

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API Title", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' followed by a space and then your JWT token."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
            });



            builder.Services.AddCors(option =>
            {
                option.AddPolicy("Allow", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();

                });

            });
            builder.Logging.AddConsole();
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
