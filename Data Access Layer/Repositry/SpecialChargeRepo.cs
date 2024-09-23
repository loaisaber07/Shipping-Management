using Data_Access_Layer.DTO.Seller;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositry
{
    public class SpecialChargeRepo : Repository<SpecialCharge>, ISpecialCharge
    {
        private readonly ShippingDataBase context;

        public SpecialChargeRepo(ShippingDataBase context):base(context)
        {
            this.context = context;
        }
        public async Task BulkInsert(IEnumerable<SpecialCharge> DTO)
        {
           await context.specialCharges.AddRangeAsync(DTO); 
        }

        public  SpecialCharge?  GetSpecialCharge(string sellerId, int city ,out bool IsExist)
        {
    SpecialCharge? special=   context
                 .specialCharges
                 .FirstOrDefault(s => s.CityID == city && s.SellerID == sellerId);
            if (special is null) { 
            IsExist = false;
                return null;
            } 
            IsExist = true;
            return special;
            
        }
    }
}
