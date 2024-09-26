using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO.Order
{
    public  class RejectDTO
    {
        [Required]
        public string Message { get; set; }
        [Required]
        public int OrderId { get; set; }
    }
}
