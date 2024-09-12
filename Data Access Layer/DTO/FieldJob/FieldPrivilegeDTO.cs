using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO.FieldJob
{
    public  class FieldPrivilegeDTO
    {
        public int PrivilegeID { get; set; }
        public string Name { get; set; }
        public bool Add { get; set; }
        public bool Delete { get; set; }
        public bool Display { get; set; }
        public bool Edit { get; set; }
    }
}
