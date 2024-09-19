using Data_Access_Layer.DTO.FieldJob;
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
    public class FieldJobRepository:Repository<FieldJob> , IFieldJob
    {
        private readonly ShippingDataBase context;

        public FieldJobRepository(ShippingDataBase context):base(context)
        {
            this.context = context;
        }

        public IEnumerable<FieldJobDTO> GetAllFieldWithPrivilege()
        {
     return       context.fieldJobs.Include(s => s.FieldPrivilege)
                .ThenInclude(s=>s.Privilege)
                .AsSplitQuery()
                .Select(s => new FieldJobDTO
            {
                ID = s.ID,
                Name = s.Name,
                DateAdding=s.DateAdding,
                FieldPrivilegeDTO = s.FieldPrivilege.Select(s => new FieldPrivilegeDTO
                {
                    Name = s.Privilege.Name,
                    PrivilegeID = s.Privilege.ID,
                    Add=s.Add,
                    Delete=s.Delete,
                    Display=s.Display,
                    Edit=s.Edit
                }).ToList()

            });
        }

        public FieldJob? GetByName(string name)
        {
         return  context.fieldJobs.FirstOrDefault(s => s.Name == name); 
        }

        public async Task<FieldJob?> GetFieldJobById(int id)
        {
        return   await context.fieldJobs
                .AsNoTracking()
                .Include(s => s.FieldPrivilege)
                .ThenInclude(s => s.Privilege)
                .AsSplitQuery()
                .FirstOrDefaultAsync(s => s.ID == id); 
        }

        public bool IsExist(string name)
        {
      FieldJob? f= context.fieldJobs.
                AsNoTracking()
                .FirstOrDefault(s => s.Name == name);
            if (f is not null) {
                return true; 
            }
            return false;
        }

        public async Task<bool> IsExistByIdAsync(int id)
        {
       FieldJob? result=    await context.fieldJobs.FirstOrDefaultAsync(s => s.ID == id);
            if (result is null) {
                return false; 
            }
            return true;
        }
    }
}
