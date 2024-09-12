using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entity
{
    public class FieldJob
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required] 
        public string Name { get; set; }
        public DateTime DateAdding { get; set; }= DateTime.Now;  //Read only property that is delivered filed holding the date of adding

        #region mapping relation between fieldJob  and application user 
        [InverseProperty("FieldJob")]
        public virtual ICollection<ApplicationUser> Users { get; set; }
        #endregion

        #region mapping relation between fieldjob and privilege
        [InverseProperty("FieldJob")]
        public virtual ICollection<FieldPrivilege> FieldPrivilege { get; set; }
        #endregion

    }
}
