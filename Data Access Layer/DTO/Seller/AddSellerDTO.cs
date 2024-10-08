﻿using Data_Access_Layer.Custom_Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO.Seller
{
    public class AddSellerDTO
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
        public string StoreName { get; set; }
        public string Govern { get; set; }
        public string City { get; set; }
        public int StoreCityId { get; set; }
        public int PickUp { get; set; } = 0;
        public int ValueOfRejectedOrder { get; set; } = 0;
        public ICollection<CitySeller>? citySellers { get; set; }

    }
}
