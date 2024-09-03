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
        public ShippingDataBase(DbContextOptions<ShippingDataBase> option) :base(option)
        {
            
        }
    }
}
