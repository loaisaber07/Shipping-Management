using Data_Access_Layer.DTO.Privilege;
using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class Privilege_Service
    {
        public static Privilege AddPrivilege(AddPrivilegeDTO dto)
        {
            Privilege privilege = new Privilege
            {
                Name = dto.Name,
            };
            return privilege;
        }
        public static Privilege EditPrivilege(EditPrivilegeDTO dto)
        {
            Privilege privilege = new Privilege
            {
                ID = dto.ID,
                Name = dto.Name,
                
            };
            return privilege;
        }
        public static EditPrivilegeDTO GetPrivilege(Privilege privilege)
        {
            EditPrivilegeDTO editPrivilege = new EditPrivilegeDTO
            {
                Name = privilege.Name,
                ID = privilege.ID
            };
            return editPrivilege;
        }
        public static IEnumerable<EditPrivilegeDTO> GetPrivileges(IEnumerable<Privilege> privilege)
        {
            List<EditPrivilegeDTO> editPrivileges = new List<EditPrivilegeDTO>();
            foreach(Privilege pr in privilege)
            {
                EditPrivilegeDTO privilegeDTO = new EditPrivilegeDTO
                {
                    ID = pr.ID,
                    Name = pr.Name
                };
                editPrivileges.Add(privilegeDTO);
            }
            return editPrivileges;
        }
    }
}
