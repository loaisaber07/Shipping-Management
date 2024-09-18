using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO.Seller
{
    public  class CitySeller
    {
        [Required]
        public int CityId { get; set; }
        [Required]
        public int SpecialCharge { get; set; }
    }
}
