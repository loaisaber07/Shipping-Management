using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO.FieldJob
{
    public  class FieldJobDTO
    {
        public int ID { get; set; }
        public string  Name { get; set; }
        public ICollection<FieldPrivilegeDTO> FieldPrivilegeDTO { get; set; }

    }
}
