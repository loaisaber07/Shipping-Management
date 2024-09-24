using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO.Order
{
    public class EmployeeUpdateOrderStatusDTO
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public int OrderStatusID { get; set; }
    }
}
