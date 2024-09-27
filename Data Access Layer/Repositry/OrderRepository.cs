using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                        .Include(s => s.Products)
                        .Include(s=>s.City)
                        .Include(s=>s.Govern);
        }

        public async Task<Order?> GetById(int id)
        {
           Order? order = await dataBase
                .Orders
                .Include(o=>o.Products)
                .Include(g=>g.Govern)
                .Include (s=>s.City)
                .FirstOrDefaultAsync(o=>o.ID==id);
            return order;
        }

        public IQueryable<Order> GetOrderByBranch(int? branchId)
        { 
            dataBase.Orders
                .AsNoTracking()
                .Include(s=>s.Products)
                .Where(s => s.BranchID == branchId);
            return dataBase.Orders;
        }

        public async Task<IEnumerable<Order?>> GetOrderByTimeAdding(DateTime begin, DateTime end, int statusId)
        {
        return    await dataBase.Orders
                .Where(s => s.DateAdding >= begin && s.DateAdding <= end && s.OrderStatusID == statusId)
                .Include(o => o.Seller)
                .Include(s=>s.Agent)
                .Include(s=>s.Agent)
                .ThenInclude(t=>t.TypeOfOffer)
                .Include(o => o.OrderStatus)
                .Include(c => c.City)
                .Include(s => s.Govern)
                .Include(t => t.TypeOfCharge)
                .Include(t => t.TypeOfReceipt)
                .AsSplitQuery()
                .ToListAsync();

        }

        public async Task<IEnumerable<Order?>> GetOrderForAdmin()
        {
            return await dataBase.Orders
                     .AsNoTracking()
                     .Include(s => s.OrderStatus)
                     .ToListAsync();
        }

        public async  Task<Order?> GetOrderForGetChargeCost(int id)
        {
 return    await     dataBase.Orders
                  .Include(o => o.Seller)
                  .Include(s => s.Agent)
                  .Include(s => s.Agent)
                  .ThenInclude(t => t.TypeOfOffer)
                  .Include(o => o.OrderStatus)
                  .Include(c => c.City)
                  .Include(s => s.Govern)
                  .Include(t => t.TypeOfCharge)
                  .Include(t => t.TypeOfReceipt)
                  .AsSplitQuery()
                  .FirstOrDefaultAsync(s=>s.ID ==id);
        }

        public async Task<Order?> GetOrderForShippinCost(int id)
        {
            Order? order = await dataBase.Orders
                .AsNoTracking()
                .Include(o=>o.Seller)
                .Include(o=>o.OrderStatus)
                .Include(c=>c.City)
                .Include(s=>s.Govern)
                .Include(t=>t.TypeOfCharge)
                .Include(t=>t.TypeOfReceipt)
                .FirstOrDefaultAsync (o=>o.ID==id);
            return order;
        }

        public async Task<IEnumerable<Order?>> GetOrderForSpecificAgent(string agentId)
        {
      return  await   dataBase.Orders 
                .AsNoTracking()
                .Include(s=>s.OrderStatus)
                .Where(s=>s.AgentID ==agentId)
                .ToListAsync();
        }

        public IQueryable<Order> GetOrdersByAgentId(string? agentId)
        {
          return  dataBase
                .Orders
               .AsNoTracking()
               .Include(s => s.Products)
               .Where(s => s.AgentID == agentId);
        }

        public   IQueryable<Order> GetOrdersBySellerId(string? sellerId)
        {
            return       dataBase
                         .Orders
                         .AsNoTracking()
                         .Include(s => s.Products)
                         .Where(s => s.SellerID == sellerId);
                        
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
