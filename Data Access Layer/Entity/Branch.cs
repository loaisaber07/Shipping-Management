using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entity
{
    public  class Branch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        public DateTime DataAdding { get; set; } = DateTime.Now;

        #region  
        [InverseProperty("Branch")]
        public virtual ICollection<Agent> Agents { get; set; }  
        #endregion

        #region mapping the relation between product and branch
        [InverseProperty("Branch")]
      public virtual ICollection<Order> Orders { get; set; }
        #endregion

        #region mapping the relation between employee and branch 
        [InverseProperty("Branch")]
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }  
        #endregion
    }
}
