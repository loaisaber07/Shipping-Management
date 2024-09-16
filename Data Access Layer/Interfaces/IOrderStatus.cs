using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Interfaces
{
    public interface IOrderStatus:IRepositry<OrderStatus>
    {
        Task<bool> IsExistByName(string name);
        Task<OrderStatus?> GetByName(string name);
    }
}
