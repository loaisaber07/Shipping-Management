using Data_Access_Layer.DTO.Seller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.SpecialCharge
{
    public  class SpecialChargeService
    {
        public static IEnumerable<Data_Access_Layer.Entity.SpecialCharge> GetSpecialCharges(string sellerId , IEnumerable<CitySeller> dtos) { 
        List<Data_Access_Layer.Entity.SpecialCharge> result =new List<Data_Access_Layer.Entity.SpecialCharge>();
            foreach (var item in dtos) {
                result.Add(new Data_Access_Layer.Entity.SpecialCharge
                {
                 CityID=item.CityId , 
                  SpecialChargeForSeller=item.SpecialCharge , 
                  SellerID=sellerId
                });  
            }
            return result;
        
        }
    }
}
