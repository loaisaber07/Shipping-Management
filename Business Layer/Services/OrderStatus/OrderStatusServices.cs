using Data_Access_Layer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.OrderStatus
{
    using Data_Access_Layer.Entity;
    public class OrderStatusServices
    {
        public static IEnumerable<GetOrderStatusDTO> MappingOrderStatus(IEnumerable<OrderStatus>status)
        {
            List<GetOrderStatusDTO> result = new List<GetOrderStatusDTO>();
            foreach (var item in status)
            {
                GetOrderStatusDTO statusDTO = new GetOrderStatusDTO
                {
                    Name = item.Name,
                    Id = item.ID
                };
                result.Add(statusDTO);
            }
            return result;
        }
        public static GetOrderStatusDTO OrderStatusDTO(OrderStatus status)
        {
            
            GetOrderStatusDTO get = new GetOrderStatusDTO
            {
                Id = status.ID,
                Name = status.Name,
            };
            return get;
        }


    }
}
