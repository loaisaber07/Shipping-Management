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

            ApplicationUser? entity = await userManager.Users
                .Include(s => s.FieldJob)
                .Include(s=>s.Branch)
                .FirstOrDefaultAsync(u => u.Id == id);
            if(entity is null) 
            {
                return null; 
            }
           bool checkRole=  await userManager.IsInRoleAsync(entity, "Employee");
            if (!checkRole) {
                return null;
            }
            return entity;
        }
        public async Task<bool> DeleteUserAsync(string id)
        {
            ApplicationUser? entity = await userManager.FindByIdAsync(id);
            if (entity is not null)
            {
                try
                {
                    bool checkRole = await userManager.IsInRoleAsync(entity, "Employee");
                    if (!checkRole)
                    {
                        return false;
                    }
                    await userManager.DeleteAsync(entity);
                    return true;
                }
                catch (Exception ex)
                {
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

        public async Task<IEnumerable<ApplicationUser>> GetAllEmployee()
        {
            var users = await userManager.Users.
                Include(s => s.FieldJob)
                .Include(s => s.Branch)
                .ToListAsync();
            var RoleInList = new List<ApplicationUser>();
            foreach (var user in users) {
                if (await userManager.IsInRoleAsync(user, "Employee")) { 
                RoleInList.Add(user);
                }
            }
            return RoleInList;
            
        }
        public async Task<bool> DeleteSellerAsync(string id)
        {
            ApplicationUser? entity = await userManager.FindByIdAsync(id);
            if (entity is not null)
            {
                try
                {
                    bool checkRole = await userManager.IsInRoleAsync(entity, "Seller");
                    if (!checkRole)
                    {
                        return false;
                    }
                    await userManager.DeleteAsync(entity);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;
        }

        public async Task<Seller?> GetSellerAsyncById(string id)
        {
            Seller? user = await db.sellers
                 .Include(s => s.Orders)
                 .Include(s => s.SpecialCharges)
                 .FirstOrDefaultAsync(s => s.Id == id);


            if (user is not null)
            {
                bool chickRole = await userManager.IsInRoleAsync(user, "Seller");
                if (chickRole)
                {
                    return user;
                }
            }
            return null;
        }
        public async Task<IEnumerable<Seller>> GetAllSellers()
        {
            List<ApplicationUser> users = await userManager.Users.ToListAsync();
            List<Seller> sellers = new List<Seller>();
            foreach (ApplicationUser user in users)
            {
                if (await userManager.IsInRoleAsync(user, "Seller"))
                {

                    Seller seller = (Seller)user;
                    sellers.Add(seller);
                }
            }
            return sellers;

        }





        public async Task<bool> UpdateUser(ApplicationUser user)
        {
    var result= await userManager.UpdateAsync(user);
            if (result.Succeeded) {
                return true;
            }
            return false;
        }

        public async Task<bool> CreateSeller(Seller seller, string password)
        {
   IdentityResult result = await userManager.CreateAsync(seller, password);
            if (result.Succeeded) { 
          IdentityResult r= await userManager.AddToRoleAsync(seller, "Seller");
                if (r.Succeeded) {
                    return true;
                }
            
            }
            return false;
        }

        public async Task<bool> CreateAgent(Agent agent, string password)
        {
            IdentityResult result =await userManager.CreateAsync(agent, password);
            if (result.Succeeded)
            {
                IdentityResult identityResult = await userManager.AddToRoleAsync(agent, "Agent");
                    if(identityResult.Succeeded)
                    {
                        return true;
                    }
            }
            return false;
        }
    }
}
