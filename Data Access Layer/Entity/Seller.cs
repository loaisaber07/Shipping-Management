﻿ using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entity
{
    public  class Seller :ApplicationUser
    { 

        [Required]
        public string StoreName { get; set; }
        public int PickUp { get; set; } = 0;
        public int ValueOfRejectedOrder { get; set; } = 0;



        #region mapping the relation between seller and Order 
        [InverseProperty("Seller")]
        public virtual ICollection<Order> Orders { get; set; }
        #endregion
        #region mapping the relation between City and Seller (SpecialCharge) 
        [InverseProperty("Seller")]
        public virtual ICollection<SpecialCharge> SpecialCharges { get; set; }
        #endregion
        #region mapping the relation between seller and City
        [ForeignKey("StoreCity")]
        public int StoreCityId { get; set; }
        [ForeignKey("StoreCityId")]
        public virtual City StoreCity { get; set; }
        #endregion
    }
}
