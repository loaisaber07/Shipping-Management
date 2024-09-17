using Data_Access_Layer.Custom_Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO
{
    public class AddCityDTO
    {

        [Required]
        [UniqueCityName]
        public string Name { get; set; }
        [Required]
        public int NormalCharge { get; set; }
        [Required]
        public int PickUpCharge { get; set; }

        public int? SpecialChargeForSeller { get; set; }
        public int GovernID { get; set; }
    }
}
