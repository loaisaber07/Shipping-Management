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
    public  class Govern
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        //[UniqueGovernName]
        public string Name { get; set; }
        public bool Status { get; set; } = true; 

       

        #region  mapping the relation between goven and city
        [InverseProperty("Govern")]
        public virtual ICollection<City> Cities { get; set; }
        #endregion

        #region MAPPING between govern and agent
        [InverseProperty("Govern")]
        public virtual ICollection<Agent> Agents { get; set; }
        #endregion

        #region mapping the relation between product and govern
        [InverseProperty("Govern")]
       public virtual ICollection<Order> Orders { get; set; }
        #endregion
    }
}
