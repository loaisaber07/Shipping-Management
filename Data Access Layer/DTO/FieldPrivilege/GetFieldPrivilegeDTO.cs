using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO.FieldPrivilege
{
    public class GetFieldPrivilegeDTO
    {
        public int PrivilegeID { get; set; }
        public int FieldJobID { get; set; }
        public string FieldName { get; set; }
        public string PrivilegeName { get; set;}
        public bool Add { get; set; }
        public bool Delete { get; set; }
        public bool Display { get; set; }
        public bool Edit { get; set; }
    }
}
