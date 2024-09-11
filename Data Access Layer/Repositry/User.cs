using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositry
{
    public class User:Repository<User>,IUser
    {
        private readonly ShippingDataBase db;
        private readonly UserManager<ApplicationUser> userManager;

        public User(ShippingDataBase db,UserManager<ApplicationUser> userManager):base(db)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task<ApplicationUser?> GetUserAsyncById(string id)
        {
            ApplicationUser? entity = await userManager.FindByIdAsync(id);
            return entity;
        }
        public async Task<bool> DeleteUserAsync(string id)
        {
            ApplicationUser? entity = await userManager.FindByIdAsync(id);
            if (entity is not null )
            {
                try
                {

                    db.Remove(entity);
                    return true; 
                }
                catch (Exception ex) {
                    return false; 
                }
            }
            return false;
        }

        public async Task<ApplicationUser?> GetByEmail(string email)
        {
   var user =await  userManager.FindByEmailAsync(email); 
            return user;    
        }
    }
}
