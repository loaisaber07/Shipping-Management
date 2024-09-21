using Data_Access_Layer.DTO.Order;
using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.Products
{
    public  class ProductService
    {
        public static IEnumerable<Product> MappingProduct(int orderId,IEnumerable<AddProductDTO> Dtos) {
        List<Product> list = new List<Product>();
            foreach (var dto in Dtos) {
                list.Add(new Product
                {
                    Name = dto.Name,
                    OrderID = orderId,
                    Quantity = dto.Quantity,
                    Weight = dto.ProductWeight

                });
            }
            return list; 

        }
    }
}
