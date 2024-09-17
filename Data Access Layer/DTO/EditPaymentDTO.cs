using Data_Access_Layer.Custom_Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO
{
    public class EditPaymentDTO
    {
        public int Id { get; set; }
        [AllowedValues("Prepaid", "Exchange", "CashOnDelivery")]
        [UniqueTypeOfPayment]
        public string Name { get; set; }
    }
}
