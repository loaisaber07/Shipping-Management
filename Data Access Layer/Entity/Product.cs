using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entity
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; } 
        public int Weight { get; set; }

        #region mapping the relation between product and order
        [ForeignKey("Order")]

        public int OrderID { get; set; }
        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }
        #endregion
    }
}
