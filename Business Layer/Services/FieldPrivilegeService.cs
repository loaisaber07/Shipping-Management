using Data_Access_Layer.DTO.FieldJob;
using Data_Access_Layer.DTO.FieldPrivilege;
using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public  class FieldPrivilegeService
    {
        public static IEnumerable<FieldPrivilege> CreateListOfFieldPrivilege(int fieldID, IEnumerable<AddFieldPrivilegeDTO> fieldPrivilege)
        { 
        List<FieldPrivilege> list = new List<FieldPrivilege>();
            foreach (var f in fieldPrivilege) {
                list.Add(new FieldPrivilege
                {
FieldJobID=fieldID , 
PrivilegeID=f.PrivilegeID,
Add=f.Add,
Display=f.Display,
Delete=f.Delete,
Edit=f.Edit
                });
            }
            return list;
        }


        public static IEnumerable<FieldPrivilege> EditListOfFieldPrivilege(EditFieldPrivilege fieldPrivilege)
        {
            List<FieldPrivilege> list = new List<FieldPrivilege>();
            foreach (var f in fieldPrivilege.FieldPrivilegeCollection)
            {
                list.Add(new FieldPrivilege
                {
                    FieldJobID = fieldPrivilege.ID,
                    PrivilegeID = f.PrivilegeID,
                    Add = f.Add,
                    Delete = f.Delete,
                    Edit = f.Edit,
                    Display = f.Display
                });
            }
            return list;
        }
        public static IEnumerable<GetFieldPrivilegeDTO> GetAllFieldPrivileg(IQueryable<FieldPrivilege> FB)
        {
            List<GetFieldPrivilegeDTO> result = new List<GetFieldPrivilegeDTO>();
            foreach (var f in FB)
            {
                result.Add(new GetFieldPrivilegeDTO
                {
                    FieldJobID = f.FieldJobID,
                    PrivilegeID = f.PrivilegeID,
                    Add = f.Add,
                    Display = f.Display,
                    Delete = f.Delete,
                    Edit = f.Edit,
                    FieldName = f.FieldJob.Name,
                    PrivilegeName = f.Privilege.Name

                });
            }
            return result;
        }

    }
}
