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
    public  class TypeOfPayment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
       // [AllowedValues("Prepaid","Exchange","CashOnDelivery")]
       // [UniqueTypeOfPayment]
        public string Name { get; set; }

        #region mapping the relation between product and typeofPayment
        [InverseProperty("TypeOfPayment")]
        public virtual ICollection<Order> Orders { get; set; }
        #endregion
    }
}
