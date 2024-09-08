using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entity
{
    public  class OrderStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        
        public string Name { get; set; } = "new"; 

        #region mapping the relation between this and product
        [InverseProperty("OrderStatus")]
      public virtual ICollection<Order> Orders { get; set; }


        #endregion

    }
}
