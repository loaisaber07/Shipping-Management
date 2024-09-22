using Data_Access_Layer.Custom_Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO.Agent
{
    public class AddAgentDTO
    {
        [Required]
        [UniqueUserName]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^01(0|1|2|5)\d{8}$", ErrorMessage = "Invalid phone number")]
        [UniquePhoneNumber]
        public string Phone { get; set; }
        [Required]
        public int BranchID { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public int ThePrecentageOfCompanyFromOffer { get; set; }
        [Required]
        public int GovernID { get; set; }
        [Required]
        public int TypeOfOfferID { get; set; }
    }
}
