using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO.Order
{
    public class UpdateOrderDTO
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string ClientName { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required]
        [RegularExpression(@"^01(0|1|2|5)\d{8}$", ErrorMessage = "Invalid phone number")]
        public string ClientNumber { get; set; }
        [RegularExpression(@"^01(0|1|2|5)\d{8}$", ErrorMessage = "Invalid phone number")]
        public string? ClientNumber2 { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int Cost { get; set; }
        [Required]
        public bool IsForVillage { get; set; }

        public string? Note { get; set; }
        [Required]
        public int Weight { get; set; }

        public string VillageOrStreet { get; set; }
        [Required]
        public int BranchID { get; set; }
        [Required]

        public int GovernID { get; set; }
        [Required]
        public int CityID { get; set; }
        [Required]
        public int TypeOfPaymentID { get; set; }
        [Required]

        public int TypeOfChargeID { get; set; }
        [Required]
        public int OrderStatusID { get; set; }
        [Required]
        public int TypeOfReceiptID { get; set; }
        [Required]
        public List<EditProductDTO> ProductList { get; set; }
    }
}
