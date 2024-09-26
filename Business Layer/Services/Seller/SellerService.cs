using Business_Layer.DTO.Employee;
using Data_Access_Layer.DTO.Seller;
using Data_Access_Layer.Entity;
using Microsoft.EntityFrameworkCore;
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
                StoreCityId=dto.StoreCityId,
                StoreName=dto.StoreName,
                PickUp=dto.PickUp,
                ValueOfRejectedOrder=dto.ValueOfRejectedOrder
            };
            return user;
        }
        public static GetSellerDTO GetSellerDTO(Data_Access_Layer.Entity.Seller seller)
        {
            GetSellerDTO dto = new GetSellerDTO
            {
                BranchID=seller.BranchID,
                StoreName=seller.StoreName,
                Email=seller.Email,
                Id=seller.Id,
                Govern=seller.Govern,
                Name=seller.UserName,
                Phone=seller.PhoneNumber,
                ValueOfRejectedOrder = seller.ValueOfRejectedOrder,
                PickUp = seller.PickUp , 
                BranchName = seller.Branch.Name

            };
            return dto;
        }
        public static IEnumerable<GetSellerDTO> GetAllSellers(IEnumerable<Data_Access_Layer.Entity.Seller>sellerList)
        {
            List<GetSellerDTO> getSellerDTOs = new List<GetSellerDTO>();
            foreach (var dto in sellerList)
            {
                GetSellerDTO getSeller = new GetSellerDTO
                {
                    BranchID = dto.BranchID,
                   
                    StoreName = dto.StoreName,
                    Email = dto.Email,
                    Id = dto.Id,
                    Govern = dto.Govern,
                    Name = dto.UserName,
                    Phone = dto.PhoneNumber,
                    PickUp = dto.PickUp , 
                    BranchName = dto.Branch.Name
                };
                getSellerDTOs.Add(getSeller);
            }
            return getSellerDTOs;
        }
        public static  IEnumerable<DisplayScreenForSeller?>  GetDisplayScreenForSellers(Data_Access_Layer.Entity.Seller seller) {
            List<DisplayScreenForSeller> list= new List<DisplayScreenForSeller>();
            foreach (var orders in seller.Orders) {
                list.Add(new DisplayScreenForSeller
                {
StatusName = orders.OrderStatus.Name,
StatusId=orders.OrderStatus.ID
                }); 
            }
            return list; 
        }
    }
}
