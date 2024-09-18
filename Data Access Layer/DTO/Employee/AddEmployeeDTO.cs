using Data_Access_Layer.Custom_Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTO.Employee
{
    public  class AddEmployeeDTO
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
        public int FieldJobID { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Govern { get; set; }
        public string City { get; set; }



    }
}
