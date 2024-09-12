using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO.FieldJob
{
    public  class AddFieldPrivilegeDTO
    {
        [Required]
        public int PrivilegeID { get; set; }
        [Required]
        public bool Add { get; set; }
        [Required]
        public bool Delete { get; set; }
        [Required]
        public bool Display { get; set; }
        [Required]
        public bool Edit { get; set; }
    }
}
