﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entity
{
    public  class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]

        public string ClientName { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required]
        public int ClientNumber { get; set; }
        [DataType(DataType.PhoneNumber)]
        public int? ClientNumber2 { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public int Cost { get; set; }
        [Required]
        public bool IsForVillage { get; set; } = false;
        
        public string? Note { get; set; }
        [Required]
        public int Weight { get; set; }

        public string? VillageOrStreet { get; set; }

        #region mapping the relation between product and branch 
        [ForeignKey("Branch")]
        public int BranchID { get; set; }
        [ForeignKey("BranchID")]
        public virtual Branch Branch { get; set; }
        #endregion

        #region mapping the relation product and govern
        [ForeignKey("Govern")]
        public int GovernID { get; set; }
        [ForeignKey("GovernID")]
        public virtual  Govern Govern  { get; set; }
        #endregion

        #region mapping the relation between product and seller 
        [ForeignKey("Seller")]
        public int SellerID { get; set; }
        [ForeignKey("SellerID")]

        public virtual Seller Seller { get; set; }
        #endregion

        #region mapping the relation between product and typeofPayment 
        [ForeignKey("TypeOfPayment")]
        public int TypeOfPaymentID { get; set; }
        [ForeignKey("TypeOfPaymentID")]
        public virtual TypeOfPayment TypeOfPayment { get; set; }
        #endregion

        #region mapping the relation between this and typeofcharge
        [ForeignKey("TypeOfCharge")]
        public int TypeOfChargeID { get; set; }
        [ForeignKey("TypeOfChargeID")]
        public virtual TypeOfCharge TypeOfCharge { get; set; }
        #endregion
    }
}
