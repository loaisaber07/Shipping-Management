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
    public  class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public int NormalCharge { get; set; }
        [Required]
        [UniqueCityName]
        public string Name { get; set; }
        [Required]
        public int PickUpCharge { get; set; }

        public int? SpecialChargeForSeller { get; set; }

        #region  mapping the relation between city and govern 
        [ForeignKey("Govern")]
        public int GovernID { get; set; }
        [ForeignKey("GovernID")]
        public virtual Govern Govern { get; set; }
        #endregion
        #region mapping between order and city 
        [InverseProperty("City")]
        public virtual ICollection<Order>? Orders { get; set; }
        #endregion
    }
}
