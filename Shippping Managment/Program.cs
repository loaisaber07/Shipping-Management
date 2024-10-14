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
using Shippping_Managment.RepositoyContainer;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

namespace Shippping_Managment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddJsonOptions(op => {
                    op.JsonSerializerOptions.PropertyNamingPolicy = null;
                    op.JsonSerializerOptions.PropertyNameCaseInsensitive = true; 
                    op.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });
            builder.Services.AddApiVersioning(op => { 
            op.DefaultApiVersion=new Microsoft.AspNetCore.Mvc.ApiVersion(1,0);
                op.AssumeDefaultVersionWhenUnspecified = true; 
            
            }); 
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ShippingDataBase>(option => {
                option.UseSqlServer(builder.Configuration.GetConnectionString("default"));
            }); 
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ShippingDataBase>();
            builder.Services.AddScoped<ICity,CityRepository>();

            //builder.Services.AddScoped<IGovern,GovernRepository>();
            //builder.Services.AddScoped<IBranch, BranchRepository>();
            //builder.Services.AddScoped<IFieldJob, FieldJobRepository>();
            //builder.Services.AddScoped<IFieldPrivilege, FiledPrivilegeRepository>();
            //builder.Services.AddScoped<IPrivilege, PrivilegeRepository>();
            //builder.Services.AddScoped<IUser, User>();
            //builder.Services.AddScoped<IProduct, ProductRepository>();
            //builder.Services.AddScoped<ITypeOfPayment, TypeOfPaymentRepository>();
            //builder.Services.AddScoped<ITypeOfCharge, TypeOfChargeRepository>();
            //builder.Services.AddScoped<IOrderStatus, OrderStatusRepository>();
            //builder.Services.AddScoped<IOrder,OrderRepository>();
            //builder.Services.AddScoped<ISpecialCharge, SpecialChargeRepo>();
            //builder.Services.AddScoped<ISeller, SellerRepository>();
            //builder.Services.AddScoped<IWeight,WeightRepository>();
            //builder.Services.AddScoped<ITypeOfReceipt, TypeOfReceiptRepository>();
            //builder.Services.AddScoped<ITypeOfOffer,TypeOfOfferRepository>();
            //builder.Services.AddScoped<IAgent, AgentRepository>();
            //builder.Services.AddScoped<InvoiceService>(); 
            builder.Services.AddRepository();
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
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        // Read the JWT token from the HttpOnly cookie named "jwt"
                        var token = context.Request.Cookies["jwt"];
                        if (!string.IsNullOrEmpty(token))
                        {
                            context.Token = token;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOrEmployee",policy=>policy.RequireAssertion(context=>
                context.User.HasClaim(c=>c.Type==ClaimTypes.Role&&(c.Value=="Employee"||c.Value=="Admin"))));
                options.AddPolicy("AdminOrSeller", policy => policy.RequireAssertion(context =>
                  context.User.HasClaim(c => c.Type == ClaimTypes.Role && (c.Value == "Seller" || c.Value == "Admin"))));
                options.AddPolicy("AdminOrAgent", policy => policy.RequireAssertion(context =>
                context.User.HasClaim(c => c.Type == ClaimTypes.Role && (c.Value == "Agent" || c.Value == "Admin"))));
                options.AddPolicy("Admin",policy=>policy.RequireAssertion(context=>context.User.HasClaim(c=>c.Type==ClaimTypes.Role&&c.Value=="Admin")));
                options.AddPolicy("Seller",policy=>policy.RequireAssertion(context=>context.User.HasClaim(c=>c.Type==ClaimTypes.Role&&c.Value=="Seller")));
                options.AddPolicy("Employee", policy => policy.RequireAssertion(context => context.User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "Employee")));
                options.AddPolicy("Agent", policy => policy.RequireAssertion(context => context.User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "Agent")));



            });

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shipping Managment", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your JWT token"
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
                    builder.SetIsOriginAllowed(origin => true)
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();

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
