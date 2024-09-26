using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entity
{
    public  class ShippingDataBase : IdentityDbContext<ApplicationUser>
    {
        public  object User;  

        public ShippingDataBase(DbContextOptions<ShippingDataBase> option) :base(option)
        {
            
        }

        public DbSet<Agent> agents { get; set; }
        public DbSet<Branch> branches { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<FieldJob> fieldJobs { get; set; }
        public DbSet<Govern> governs { get; set; }
        public DbSet<Privilege> privileges { get; set; }
        public DbSet<SpecialCharge> specialCharges { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> productStatuses { get; set; }
        public DbSet<Seller> sellers { get; set; }
        public DbSet<TypeOfCharge> typeOfCharges { get; set; }
        public DbSet<TypeOfOffer> typeOfOffers { get; set; }
        public DbSet<TypeOfPayment> typeOfPayments { get; set; }
        public DbSet<Weight> weights { get; set; }       
        public DbSet<Product> products { get; set; }
        public DbSet<FieldPrivilege> fieldPrivileges { get; set; }
        public DbSet<TypeOfReceipt> typeOfReceipts { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().Property<bool>("Status").IsRequired()
                .HasDefaultValue(true);
            builder.Entity<Order>().ToTable("Order");
            builder.Entity<OrderStatus>().HasIndex(s => s.Name).IsUnique();
            builder.Entity<Branch>().Property(s => s.DataAdding).HasDefaultValue(DateTime.Now);
            builder.Entity<FieldPrivilege>().HasKey(s => new { s.PrivilegeID, s.FieldJobID });
            builder.Entity<FieldJob>().Property(s => s.DateAdding).HasDefaultValue(DateTime.Now);
            builder.Entity<Order>().Property(s => s.DateAdding).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Entity<SpecialCharge>().HasKey(s => new { s.CityID, s.SellerID });
            builder.Entity<ApplicationUser>()
                 .HasDiscriminator<string>("UserType")
                 .HasValue<ApplicationUser>("ApplicationUser")
                 .HasValue<Seller>("Seller");
            builder.Entity<ApplicationUser>()
                .HasDiscriminator<string>("UserType")
                .HasValue<ApplicationUser>("ApplicationUser")
                .HasValue<Agent>("Agent");
            builder.Entity<ApplicationUser>()
                .HasOne(s => s.FieldJob)
                .WithMany(s => s.Users)
                .HasForeignKey(s => s.FiledJobID)
                .IsRequired(false);
            builder.Entity<ApplicationUser>()
                .HasOne(s => s.Branch)
                .WithMany(s => s.ApplicationUsers)
                .HasForeignKey(s => s.BranchID)
                .IsRequired(false);

            #region Add Manualy Data To DataBase
            #region AddAdmin

            builder.Entity<IdentityRole>().HasData(
          new IdentityRole
          {
              Id = "admin-role-id", // This should be a unique and valid ID
              Name = "Admin",
              NormalizedName = "ADMIN"
          }
      );
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "admin-user-id", // Ensure this is a valid, unique ID
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN",
                    EmailConfirmed = true,
                    PhoneNumber = "01004117527",
                    PasswordHash = hasher.HashPassword(null, "Admin@123") // Hashed password
                }
            );
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "admin-user-id", // ID of the user from AspNetUsers table
                    RoleId = "admin-role-id"  // ID of the role from AspNetRoles table
                }
            );
            #endregion
            #region Add 13 OrderStatus
            builder.Entity<OrderStatus>().HasData(new OrderStatus { 
            ID=1 , 
            Name="New", 
            });
            builder.Entity<OrderStatus>().HasData(new OrderStatus
            {
                ID = 2,
                Name = "Wating",
            });
            builder.Entity<OrderStatus>().HasData(new OrderStatus
            {
                ID = 3,
                Name = "AssignedToAgent",
            });
            builder.Entity<OrderStatus>().HasData(new OrderStatus
            {
                ID = 4,
                Name = "UnReachable",
            });
            builder.Entity<OrderStatus>().HasData(new OrderStatus
            {
                ID = 5,
                Name = "Postponed",
            });
            builder.Entity<OrderStatus>().HasData(new OrderStatus
            {
                ID = 6,
                Name = "PartiallyDelivered",
            });
            builder.Entity<OrderStatus>().HasData(new OrderStatus
            {
                ID = 7,
                Name = "Canceled",
            });
            builder.Entity<OrderStatus>().HasData(new OrderStatus
            {
                ID = 8,
                Name = "RejectedWithPayment",
            });
            builder.Entity<OrderStatus>().HasData(new OrderStatus
            {
                ID = 9,
                Name = "RejectWithPartialPayment",
            });
            builder.Entity<OrderStatus>().HasData(new OrderStatus
            {
                ID = 10,
                Name = "RejectedWithoutPayment",
            });
            builder.Entity<OrderStatus>().HasData(new OrderStatus
            {
                ID = 11,
                Name = "Delivered",
            });
            builder.Entity<OrderStatus>().HasData(new OrderStatus
            {
                ID = 12,
                Name = "Rejected",
            });
            #endregion
            #region Add  2 TypeOfOffer
            builder.Entity<TypeOfOffer>().HasData(new TypeOfOffer { 
            ID=1 , 
            Name="Precentage"
            });
            builder.Entity<TypeOfOffer>().HasData(new TypeOfOffer
            {
                ID = 2,
                Name = "FixedAmount"
            });
            #endregion
            #region Add 2 Type Of Receipt
            builder.Entity<TypeOfReceipt>().HasData(new TypeOfOffer
            {
                ID=1 , 
                Name="Branch"
            });
            builder.Entity<TypeOfReceipt>().HasData(new TypeOfOffer
            {
                ID = 2,
                Name = "Store"
            });
            #endregion
            #endregion

            base.OnModelCreating(builder);
        }
    }
}
