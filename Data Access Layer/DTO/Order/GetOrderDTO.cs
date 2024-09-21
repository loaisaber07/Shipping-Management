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
        public string ClientNumber { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string? ClientNumber2 { get; set; }
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
        public int CityID { get; set; }
        public string SellerID { get; set; }
        public int TypeOfPaymentID { get; set; }
        public int TypeOfChargeID { get; set; }
        public int OrderStatusID { get; set; }
        public int TypeOfReceiptID { get; set; }
        public ICollection<GetProductDTO>? ProductList { get; set; }
    }
}
