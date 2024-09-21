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
                TypeOfReceiptID = add.TypeOfReceiptID,
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
                TypeOfReceiptID=order.TypeOfReceiptID,
                Note= order.Note,
                CityID = order.CityID,
                ProductList = order.Products.Select(p => new GetProductDTO {
                    Id = p.ID,
                    Name = p.Name,
                    OrderId = p.OrderID,
                    ProductWeight = p.Weight,
                    Quantity = p.Quantity

                }).ToList()

            };
           
            return getOrderDTO;
        }
        public static IEnumerable<GetOrderDTO> GetAllOrder(IQueryable< Data_Access_Layer.Entity.Order> orders)
        {
           return  orders.Select(s => new GetOrderDTO
            {

                ClientName = s.ClientName,
                ClientNumber = s.ClientNumber,
                ClientNumber2 = s.ClientNumber2,
                Email = s.Email,
                Cost = s.Cost,
                BranchID = s.BranchID,
                CityID = s.CityID,
                GovernID = s.GovernID,
                Id = s.ID,
                IsForVillage = s.IsForVillage,
                Note = s.Note,
                OrderStatusID = s.OrderStatusID,
                SellerID = s.SellerID,
                TypeOfChargeID = s.TypeOfChargeID,
                TypeOfPaymentID = s.TypeOfPaymentID,
                VillageOrStreet = s.VillageOrStreet,
                TypeOfReceiptID =s.TypeOfReceiptID,
                Weight = s.Weight,
                ProductList = s.Products.Select(p => new GetProductDTO {
                Id= p.ID,
                Name= p.Name,
                OrderId= p.OrderID,
                ProductWeight= p.Weight,
                Quantity= p.Quantity
                }).ToList()


            });
        }

    }
}
