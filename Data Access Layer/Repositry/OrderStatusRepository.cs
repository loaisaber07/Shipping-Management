using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositry
{
    public class OrderStatusRepository:Repository<OrderStatus>,IOrderStatus
    {
        private readonly ShippingDataBase dataBase;

        public OrderStatusRepository(ShippingDataBase dataBase) : base(dataBase)
        {
            this.dataBase = dataBase;
        }
        public async Task<OrderStatus?> GetByName(string name)
        {
            OrderStatus? type = await dataBase.productStatuses.AsNoTracking().FirstOrDefaultAsync(p => p.Name == name);
            return type;
        }

        public async Task<bool> IsExistByName(string name)
        {
            OrderStatus? type = await dataBase.productStatuses.AsNoTracking().FirstOrDefaultAsync(p => p.Name == name);
            if (type == null)
            {
                return false;
            }
            return true;
        }
    }
}
