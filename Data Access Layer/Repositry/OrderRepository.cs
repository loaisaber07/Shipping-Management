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
    public class OrderRepository:Repository<Order>,IOrder
    {
        private readonly ShippingDataBase dataBase;

        public OrderRepository(ShippingDataBase dataBase) : base(dataBase)
        {
            this.dataBase = dataBase;
        }

        public IQueryable<Order> GetAll()
        {
            return    dataBase
                        .Orders
                        .AsNoTracking()
                        .Include(s => s.Products);
        }

        public async Task<Order?> GetById(int id)
        {
           Order? order = await dataBase
                .Orders
                .Include(o=>o.Products)
                .FirstOrDefaultAsync(o=>o.ID==id);
            return order;
        }

        public async Task<Order?> GetOrderForShippinCost(int id)
        {
            Order? order = await dataBase.Orders
                .Include(o=>o.Seller).ThenInclude(s=>s.SpecialCharges)
                .Include(c=>c.City)
                .Include(t=>t.TypeOfCharge)
                .Include(t=>t.TypeOfReceipt)
                .FirstOrDefaultAsync (o=>o.ID==id);
            return order;
        }

        public async Task<SpecialCharge?> GetSpecialForSeller(int id,string sellerId)
        {
          SpecialCharge? special =await dataBase.specialCharges.FirstOrDefaultAsync(c=>c.CityID==id&&c.SellerID== sellerId);
            return special;

        }

        public async Task<Weight?> GetWeight()
        {
           Weight? weight=  await dataBase.weights.FirstOrDefaultAsync();
            return weight;
        }

        public bool ISEXIST(int id)
        {
       int? orderId= dataBase
                      .Orders
                      .FirstOrDefault(s=> s.ID == id)?.ID;
            if (orderId is null) {
                return false; 
            } 
            return true  ;
        }
    }
}
