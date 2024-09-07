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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().Property<bool>("Status").IsRequired()
                .HasDefaultValue(true);  
            builder.Entity<Govern>().Property<bool>("Status").IsRequired().HasDefaultValue(true);
            builder.Entity<Branch>().Property<bool>("Status").IsRequired().HasDefaultValue(true);

     
            base.OnModelCreating(builder);
        }
    }
}
