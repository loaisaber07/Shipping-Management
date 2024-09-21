using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO.Order
{
    public class AddProductDTO
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int ProductWeight { get; set; }
  
        
    }
}
