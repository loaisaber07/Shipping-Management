using Business_Layer.DTO.Employee;
using Data_Access_Layer.DTO.Seller;
using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.Seller
{
    public class SellerService
    {
        public static Data_Access_Layer.Entity.Seller GetSeller(AddSellerDTO dto)
        {
            Data_Access_Layer.Entity.Seller user = new Data_Access_Layer.Entity.Seller
            {
                UserName=dto.Name,
                Email=dto.Email,
                PhoneNumber=dto.Phone,
                BranchID=dto.BranchID,
                Govern =dto.Govern,
                City=dto.City,
                StoreName=dto.StoreName,
                PickUp=dto.PickUp,
                ValueOfRejectedOrder=dto.ValueOfRejectedOrder
            };
            return user;
        }
    }
}
