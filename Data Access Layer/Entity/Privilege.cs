using Data_Access_Layer.Custom_Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entity
{
    public  class Privilege
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [UniquePrivilegeName]
        public string Name { get; set; }


        #region mapping relation between fieldjob and privilege
        [InverseProperty("Privilege")]
        public virtual ICollection<FieldPrivilege> FieldPrivilege { get; set; }
        #endregion
    }
}
