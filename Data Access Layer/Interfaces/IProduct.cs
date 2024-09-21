using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Interfaces
{
    public interface IProduct:IRepositry<Product>
    {
        public Task BulkInsert(IEnumerable<Product> products);
        Task<List<Product>> getProductsByOrderId(int orderId); 
        
    }
}
