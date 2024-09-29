using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entity
{
    [Table("Order")]
    public  class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]

        public string ClientName { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required]
        [RegularExpression(@"^01(0|1|2|5)\d{8}$", ErrorMessage = "Invalid phone number")]

        public string ClientNumber { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^01(0|1|2|5)\d{8}$", ErrorMessage = "Invalid phone number")]
        public string? ClientNumber2 { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [Range(10 , 100000)]
        public int Cost { get; set; }
        [Required]
        public bool IsForVillage { get; set; } = false;
        
        public string? Note { get; set; }
        [Required]
        public int Weight { get; set; }
        public DateTime DateAdding { get; set; }
        public string? VillageOrStreet { get; set; }
        public bool Rejected { get; set; } = false;
        public string? ReasonOfReject { get; set; } = string.Empty;
        public decimal chargeCost { get; set; } = 0;

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
        #region mapping the realtion between Order and city 
        [ForeignKey("City")]

        public int CityID { get; set; }
        [ForeignKey("CityID")]
        public virtual City City { get; set; }  
        #endregion

        #region mapping the relation between product and seller 
        [ForeignKey("Seller")]
        public string SellerID { get; set; }
        [ForeignKey("SellerID")]

        public virtual Seller Seller { get; set; }
        #endregion

        #region mapping the relation between order and agent
        [ForeignKey("Agent")]
        public string? AgentID { get; set; }
        [ForeignKey("AgentID")]
        public virtual Agent Agent { get; set; }
        #endregion

        #region mapping the relation between product and typeofPayment 
        [ForeignKey("TypeOfPayment")]
        public int TypeOfPaymentID { get; set; }
        [ForeignKey("TypeOfPaymentID")]
        public virtual TypeOfPayment TypeOfPayment { get; set; }
        #endregion

        #region mapping the relation between product and TypeOfReceipt 
        [ForeignKey("TypeOfReceipt")]
        public int TypeOfReceiptID { get; set; }
        [ForeignKey("TypeOfReceiptID")]
        public virtual TypeOfReceipt TypeOfReceipt { get; set; }
        #endregion

        #region mapping the relation between this and typeofcharge
        [ForeignKey("TypeOfCharge")]
        public int TypeOfChargeID { get; set; }
        [ForeignKey("TypeOfChargeID")]
        public virtual TypeOfCharge TypeOfCharge { get; set; }
        #endregion

        #region mapping the realtion between this and OrderStatus
        [ForeignKey("OrderStatus")]
        public int OrderStatusID { get; set; }
        [ForeignKey("OrderStatusID")]
        public virtual OrderStatus  OrderStatus { get; set; }
        #endregion
        #region mapping relation between product and order
        [InverseProperty("Order")]
        public virtual ICollection<Product> Products { get; set; }
        #endregion
    }
}
