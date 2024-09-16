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
    public class ProductRepository:Repository<Product>, IProduct
    {
        private readonly ShippingDataBase dataBase;

        public ProductRepository(ShippingDataBase dataBase) : base(dataBase)
        {
            this.dataBase = dataBase;
        }
        public async Task BulkInsert(IEnumerable<Product> products)
        {
            await dataBase.products.AddRangeAsync(products);
            await dataBase.SaveChangesAsync();
        }

        public async Task<List<Product>> getProductsByOrderId(int orderId)
        {
           List<Product> ProductList = await dataBase.products.AsNoTracking().Where(p=>p.OrderID==orderId).ToListAsync();
            return ProductList;
        }
    }
}
