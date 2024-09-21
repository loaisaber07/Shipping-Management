using Data_Access_Layer.DTO.Order;
using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business_Layer.Services.Order
{
    public class OrderService
    {
        public static Data_Access_Layer.Entity.Order MappingOrder(string sellerId ,AddOrderDTO add)
        {
            Data_Access_Layer.Entity.Order order = new Data_Access_Layer.Entity.Order
            { 
                BranchID = add.BranchID,
                ClientName = add.ClientName,
                ClientNumber = add.ClientNumber,
                ClientNumber2 = add.ClientNumber2,
                Cost = add.Cost,
                Email = add.Email,
                GovernID = add.GovernID,
                Weight = add.Weight,
                VillageOrStreet = add.VillageOrStreet,
                IsForVillage = add.IsForVillage,
                TypeOfPaymentID = add.TypeOfPaymentID,
                TypeOfChargeID = add.TypeOfChargeID,
                SellerID = sellerId,
                OrderStatusID = add.OrderStatusID,
                TypeOfReceiptID = add.TypeOfPaymentID,
                Note = add.Note,
                CityID=add.CityID
            };
            return order;
        }
        public static GetOrderDTO GetOrder(Data_Access_Layer.Entity.Order order)
        {
            GetOrderDTO getOrderDTO = new GetOrderDTO
            {
                BranchID= order.BranchID,
                ClientName = order.ClientName,
                ClientNumber = order.ClientNumber,
                ClientNumber2 = order.ClientNumber2,
                Cost = order.Cost,
                Email = order.Email,
                GovernID= order.GovernID,
                Weight = order.Weight,
                Id=order.ID,
                IsForVillage= order.IsForVillage,
                OrderStatusID= order.OrderStatusID,
                SellerID= order.SellerID,   
                TypeOfChargeID= order.TypeOfChargeID,
                VillageOrStreet= order.VillageOrStreet,
                TypeOfPaymentID= order.TypeOfPaymentID,
                Note= order.Note,
                CityID = order.CityID

            };
           
            return getOrderDTO;
        }
        public static IEnumerable<GetOrderDTO> GetAllOrder(IEnumerable< Data_Access_Layer.Entity.Order> order)
        {
           List<GetOrderDTO> dtoList = new List<GetOrderDTO>();
            foreach (var item in order)
            {
                GetOrderDTO getOrder = new GetOrderDTO
                {
                    BranchID = item.BranchID,
                    ClientName = item.ClientName,
                    ClientNumber = item.ClientNumber,
                    ClientNumber2 = item.ClientNumber2,
                    Cost = item.Cost,
                    Email = item.Email,
                    GovernID = item.GovernID,
                    Weight = item.Weight,
                    Id = item.ID,
                    IsForVillage = item.IsForVillage,
                    OrderStatusID = item.OrderStatusID,
                    SellerID = item.SellerID,
                    TypeOfChargeID = item.TypeOfChargeID,
                    TypeOfPaymentID = item.TypeOfPaymentID,
                    VillageOrStreet = item.VillageOrStreet,
                    Note = item.Note 
                };
                dtoList.Add(getOrder);
            }
            return dtoList;
        }

    }
}
