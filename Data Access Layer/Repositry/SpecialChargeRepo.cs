using Data_Access_Layer.DTO.Seller;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
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
    }
}
