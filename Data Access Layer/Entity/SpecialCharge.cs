using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entity
{
    public  class SpecialCharge
    {
        [ForeignKey("Seller")]
        public string SellerID { get; set; }
        [ForeignKey("City")]
        public int CityID { get; set; }
        [ForeignKey("SellerID")]
        public virtual  Seller Seller   { get; set; }
        [ForeignKey("CityID")]
        public virtual  City City { get; set; }
        [Required]
        public int SpecialChargeForSeller { get; set; }
    }
}
