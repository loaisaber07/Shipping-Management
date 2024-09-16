using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO
{
    public class AddTypeOfChargeDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Cost { get; set; }
    }
}
