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
    public class PrivilegeRepository:Repository<Privilege>,IPrivilege
    {
        private readonly ShippingDataBase dataBase;

        public PrivilegeRepository(ShippingDataBase dataBase):base(dataBase) 
        {
            this.dataBase = dataBase;
        }

        public async Task<string> GetNameById(int Id)
        {
            Privilege? privilege = await dataBase.privileges.AsNoTracking().FirstOrDefaultAsync(p => p.ID == Id);
            if (privilege is not null)
            {
                return privilege.Name;
            }
            return string.Empty;
        }

        public async Task<bool> IsExsitsById(int Id)
        {
           Privilege? privilege = await dataBase.privileges.AsNoTracking().FirstOrDefaultAsync(p => p.ID==Id);
            if (privilege is not null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> IsExsitsByName(string Name)
        {
          Privilege? privilege = await dataBase.privileges.AsNoTracking().FirstOrDefaultAsync(p=>p.Name==Name);
            if (privilege is not null)
            {
                return true;
            }
            return false;
        }
    }
}
