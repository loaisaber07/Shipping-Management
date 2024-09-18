using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.Privileges
{
    public class PrivilegeService
    {

      
        public static IEnumerable<FieldPrivilege> MappingFieldJob(Privilege p, IEnumerable<FieldJob> fields) {
            List<FieldPrivilege> FBS = new List<FieldPrivilege>();  
            foreach (FieldJob field in fields) {
                FBS.Add(new FieldPrivilege
                {
                    FieldJobID=field.ID,
                    PrivilegeID=p.ID
                });
            }
            return FBS; 
          
        }
    }
}
