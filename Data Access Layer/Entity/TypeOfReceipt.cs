using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entity
{
    public class TypeOfReceipt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        #region mapping the relation between product and TypeOfReceipt
        [InverseProperty("TypeOfReceipt")]
        public virtual ICollection<Order> Orders { get; set; }
        #endregion
    }
}
