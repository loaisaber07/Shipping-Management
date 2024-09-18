﻿using Microsoft.AspNetCore.Identity;
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
            builder.Entity<Order>().Property(s=>s.DateAdding).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Entity<SpecialCharge>().HasKey(s => new { s.CityID, s.SellerID });
            builder.Entity<ApplicationUser>()
                 .HasDiscriminator<string>("UserType")
                 .HasValue<ApplicationUser>("ApplicationUser")
                 .HasValue<Seller>("Seller");
            builder.Entity<ApplicationUser>()
                .HasOne(s=>s.FieldJob)
                .WithMany(s=>s.Users)
                .HasForeignKey(s=> s.FiledJobID)
                .IsRequired(false);

            
            
            base.OnModelCreating(builder);
        }
    }
}
