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
        public static ApplicationUser GetSeller(AddSellerDTO dto)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName=dto.Name,
                Email=dto.Email,
                PhoneNumber=dto.Phone,
                BranchID=dto.BranchID,
                FiledJobID=dto.FiledJobID,//?????????
                Govern =dto.Govern,
                City=dto.City,
            };
            return user;
        }
    }
}
