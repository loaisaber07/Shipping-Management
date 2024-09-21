using Data_Access_Layer.Custom_Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO.TypeOfOffer
{
    public class EditTypeOfOfferDTO
    {
        public int ID { get; set; }
        [Required]
        [UniqueTypeOfOfferEdit]
        public string Name { get; set; }
    }
}
