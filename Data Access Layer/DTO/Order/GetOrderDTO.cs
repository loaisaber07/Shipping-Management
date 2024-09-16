using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO.Order
{
    public class GetOrderDTO
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required]
        public int ClientNumber { get; set; }
        [DataType(DataType.PhoneNumber)]
        public int? ClientNumber2 { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public int Cost { get; set; }
        [Required]
        public bool IsForVillage { get; set; } = false;

        public string? Note { get; set; }
        [Required]
        public int Weight { get; set; }

        public string? VillageOrStreet { get; set; }

        public int BranchID { get; set; }

        public int GovernID { get; set; }
        public string SellerID { get; set; }
        public int TypeOfPaymentID { get; set; }
        public int TypeOfChargeID { get; set; }
        public int OrderStatusID { get; set; }
       // public List<GetProductDTO> ProductList { get; set; }
    }
}
