﻿using Data_Access_Layer.Entity;
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
        private readonly RoleManager<IdentityRole> roleManager;

        public User(ShippingDataBase db,UserManager<ApplicationUser> userManager 
           ,RoleManager<IdentityRole> roleManager):base(db)
        {
            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
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

        public async Task<bool> CreateUser(ApplicationUser user , string password)
        {
            IdentityResult result = await userManager.CreateAsync(user, password);
        return result.Succeeded;
        }

        public async Task<bool> AddRole(string Email, string roleName)
        {
        ApplicationUser? user =  await userManager.FindByEmailAsync(Email);
            if (user is null) {
                return false; 
            }
      bool result= await  roleManager.RoleExistsAsync(roleName);
            if (!result) {
                return false; 
            }
 result =await userManager.IsInRoleAsync(user , roleName); 
            if (!result) {

           IdentityResult r= await userManager.AddToRoleAsync(user,roleName);  
                return r.Succeeded;
            } 
            return false;

        }
    }
}
