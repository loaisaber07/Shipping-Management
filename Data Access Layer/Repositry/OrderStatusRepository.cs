using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositry
{
    public class OrderStatusRepository:Repository<OrderStatus>,IOrderStatus
    {
        public OrderStatusRepository(ShippingDataBase dataBase) : base(dataBase)
        {

        }
    }
}
