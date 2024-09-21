using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entity
{
    public  class Agent:ApplicationUser
    {
        
        [Required]
        public int ThePrecentageOfCompanyFromOffer { get; set; }

        #region mapping the relation between Agent and Order
        [InverseProperty("Agent")]
        public virtual ICollection<Order> Orders { get; set; }
        #endregion

        #region mapping the relation between agent and govern 
        [ForeignKey("Governs")]
        public int GovernID { get; set; }
        [ForeignKey("GovernID")]
        public virtual  Govern Governs { get; set; }
        #endregion

        #region mapping the relation between agent and typeofoffer
        [ForeignKey("TypeOfOffer")]
        public int TypeOfOfferID { get; set; }
        [ForeignKey("TypeOfOfferID")]
        public virtual TypeOfOffer TypeOfOffer { get; set; }
        #endregion
    }
}
