﻿using Data_Access_Layer.Entity;
using Data_Access_Layer.Repositry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Interfaces
{
    public interface IUser:IRepositry<User>
    {
        Task<ApplicationUser?> GetUserAsyncById(string id);
        Task<Seller?> GetSellerAsyncById(string id);
        Task<Agent?> GetAgentAsyncById(string id);
        Task<bool> DeleteUserAsync(string id);
        Task<bool> DeleteSellerAsync(string id);
        Task<bool> DeleteAgentAsync(string id);
        Task<ApplicationUser?> GetByEmail(string email); 
        Task<bool> CreateUser(ApplicationUser user,string password); 
        Task<bool> CreateSeller(Seller seller , string password);
        Task<bool>CreateAgent(Agent agent,string password);
        Task<bool> AddRole(string Email, string roleName);
        Task<IEnumerable<ApplicationUser>> GetAllEmployee();
        IEnumerable<Seller> GetAllSellers();
        Task<IEnumerable<Agent>> GetAllAgents();
        Task<bool> UpdateUser(ApplicationUser user); 


    }
}
