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
        [AllowedValues("Prepaid","Exchange","CashOnDelivery")]
        public string Name { get; set; }

        #region mapping the relation between product and typeofPayment 
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]

        public virtual Product Product { get; set; }
        #endregion
    }
}
