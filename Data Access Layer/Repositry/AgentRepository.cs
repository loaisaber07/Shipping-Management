using Data_Access_Layer.DTO.Agent;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositry
{
    public class AgentRepository:Repository<Agent>,IAgent
    {
        private readonly ShippingDataBase dataBase;

        public AgentRepository(ShippingDataBase dataBase) : base(dataBase)
        {
            this.dataBase = dataBase;
        }

        public async Task<IEnumerable<GetAgentsToAsigenOrderDTO>> GetAgent(Order order)
        {
            return await dataBase.agents.Include(a => a.Governs).AsNoTracking()
                .Where(a => a.BranchID == order.BranchID && a.GovernID == order.GovernID)
                .Select(a=>new GetAgentsToAsigenOrderDTO { 
                    ID=a.Id,
                    UserName=a.UserName,
                    Govern = a.Governs.Name
                    }).
                ToListAsync();
           
        }
    }
}
