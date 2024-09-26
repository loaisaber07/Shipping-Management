using Data_Access_Layer.DTO.Seller;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public SellerRepository(ShippingDataBase dataBase ) : base(dataBase)
        {
            this.dataBase = dataBase;
            
        }

        public async Task<Seller?> DisplayScreenForSeller(string id)
        {
            return await dataBase.sellers
                    .AsNoTracking()
                    .Include(s => s.Orders)
                    .ThenInclude(s => s.OrderStatus)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(s => s.Id == id);  
                    
                
        }
    }
}
