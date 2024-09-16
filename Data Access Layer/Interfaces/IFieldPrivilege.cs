using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Interfaces
{
    public  interface IFieldPrivilege  :IRepositry<FieldPrivilege>
    {
        Task BulkInsert(IEnumerable<FieldPrivilege> p); 
        bool BulkIUpdate(IEnumerable<FieldPrivilege> p);
        IQueryable<FieldPrivilege> GetAll();  

    }
}
