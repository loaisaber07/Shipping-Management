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
        public string Name { get; set; }
 
        public bool Add { get; set; } = false;
        public bool Edit { get; set; } = false;
        public bool Display { get; set; } = false;  
        public bool Delete { get; set; }= false;

        #region mapping relation between fieldjob and privilege
        [ForeignKey("FieldJob")]
        public int FieldJobID { get; set; }
        [ForeignKey("FieldJobID")]
        public virtual FieldJob FieldJob { get; set; }


        #endregion
    }
}
