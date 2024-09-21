using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using EFCore.BulkExtensions;
using NetTopologySuite.Operation.Distance;


namespace Data_Access_Layer.Repositry
{
    public class ProductRepository:Repository<Product>, IProduct
    {
        private readonly ShippingDataBase dataBase;

        public ProductRepository(ShippingDataBase dataBase) : base(dataBase)
        {
            this.dataBase = dataBase;
        }

        public async Task<bool>  BulkDelete(int id)
        {
            try
            {
               await dataBase.products
                  .Where(s=>s.OrderID ==id)
                  .ExecuteDeleteAsync();
            return true;
            }
            catch
            { 
            return false;
            }
        }

        public async Task BulkInsert(IEnumerable<Product> products)
        {
            await dataBase.products.AddRangeAsync(products);
            await dataBase.SaveChangesAsync();
        }

        public async Task<bool> BulkUpdate(IEnumerable<Product> products)
        {
            try {
             await  dataBase
                        .BulkUpdateAsync(products);
                        return true;
            }
            catch {
                return false;
            }
        }

        public async Task<List<Product>> getProductsByOrderId(int orderId)
        {
           List<Product> ProductList = await dataBase.products.AsNoTracking().Where(p=>p.OrderID==orderId).ToListAsync();
            return ProductList;
        }
    }
}
