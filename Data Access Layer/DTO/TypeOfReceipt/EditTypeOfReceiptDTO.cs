using Data_Access_Layer.Custom_Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO.TypeOfReceipt
{
    public class EditTypeOfReceiptDTO
    {
        public int ID { get; set; }
        [UniqueTypeOfReceiptNameEdit]
        public string Name { get; set; }
    }
}
