using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entity
{
    public  class Seller :IdentityUser
    { 

        [Required]
        public string StoreName { get; set; }
        public int? PickUp { get; set; } 
        public int? ValueOfRejectedOrder { get; set; }

        #region mapping the relation between seller govern  
        [ForeignKey("Govern")]
        public int GovernID { get; set; }
        [ForeignKey("GovernID")]
        public virtual Govern Govern { get; set; }


        #endregion

        #region mapping the relation between seller and product 
        [InverseProperty("Seller")]
        public virtual ICollection<Order> Orders { get; set; }
        #endregion
    }
}
