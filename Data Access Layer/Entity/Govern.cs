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
        public string Name { get; set; }

        #region  
        [ForeignKey("Seller")]
        public string SellerID { get; set; }
        [ForeignKey("SellerID")]
        
        public virtual Seller Seller { get; set; }

        #endregion

        #region  mapping the relation between goven and city
        [InverseProperty("Govern")]
        public virtual ICollection<City> Cities { get; set; }
        #endregion
    }
}
