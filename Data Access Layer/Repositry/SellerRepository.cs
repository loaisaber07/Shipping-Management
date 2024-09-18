using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositry
{
    public class SellerRepository:Repository<Seller>,ISeller
    {
        private readonly ShippingDataBase dataBase;
        private readonly UserManager<Seller> userManager;

        public SellerRepository(ShippingDataBase dataBase ,UserManager<Seller>userManager) : base(dataBase)
        {
            this.dataBase = dataBase;
            this.userManager = userManager;
        }

    }
}
