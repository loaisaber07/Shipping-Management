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
    public class FiledPrivilegeRepository : Repository<FieldPrivilege>, IFieldPrivilege
    {
        private readonly ShippingDataBase context;

        public FiledPrivilegeRepository(ShippingDataBase context):base(context)
        {
            this.context = context;
        }
        public async Task BulkInsert(IEnumerable<FieldPrivilege> p)
        {
       await context.fieldPrivileges.AddRangeAsync(p);
        }

        public bool BulkIUpdate(IEnumerable<FieldPrivilege> p)
        {
            try { 
            context.fieldPrivileges.UpdateRange(p);
                return true;
            }
            catch { 
            return false;
            }
            
        }

        public IQueryable<FieldPrivilege> GetAll()
        {
           return  context.fieldPrivileges.Include(s=>s.FieldJob).Include(s=>s.Privilege); 

        }
    }
}
