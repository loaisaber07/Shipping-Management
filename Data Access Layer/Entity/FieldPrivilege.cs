using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entity
{
    public class FieldPrivilege
    {
        public bool Add { get; set; } = false;
        public bool Delete { get; set; } = false;
        public bool Edit { get; set; } = false;
        public bool Display { get; set; } = false;
        [ForeignKey("FieldJob")]
        public int FieldJobID { get; set; }
        [ForeignKey("Privilege")]
        public int PrivilegeID { get; set; }
        [ForeignKey("FieldJobID")]
        public FieldJob FieldJob { get; set; }
        [ForeignKey("PrivilegeID")]
        public Privilege Privilege { get; set; }



    }
}
