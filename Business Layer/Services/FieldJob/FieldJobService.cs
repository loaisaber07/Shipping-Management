using Data_Access_Layer.DTO.FieldJob;
using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.FieldJob
{
    using Data_Access_Layer.Entity;
    public class FieldJobService
    {
        public static FieldJobDTO MappingFieldJob(FieldJob fieldjob) {
          FieldJobDTO dto = new FieldJobDTO { 
            Name = fieldjob.Name,
            DateAdding = fieldjob.DateAdding,
             ID = fieldjob.ID,
             FieldPrivilegeDTO =fieldjob.FieldPrivilege
             .Select(s => new FieldPrivilegeDTO
             {
                 Name = s.Privilege.Name,
                 PrivilegeID = s.Privilege.ID,
                 Add = s.Add,
                 Delete = s.Delete,
                 Display = s.Display,
                 Edit = s.Edit
             }).ToList()

            }; 
        return dto;
        } 
        
    }
}
