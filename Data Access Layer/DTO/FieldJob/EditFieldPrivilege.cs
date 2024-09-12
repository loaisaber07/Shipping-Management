using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO.FieldJob
{
    public class EditFieldPrivilege
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<AddFieldPrivilegeDTO> FieldPrivilegeCollection{ get; set; }


    }
}
