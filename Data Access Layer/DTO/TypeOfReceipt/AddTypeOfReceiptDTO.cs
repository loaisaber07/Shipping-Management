﻿using Data_Access_Layer.Custom_Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO.TypeOfReceipt
{
    public class AddTypeOfReceiptDTO
    {
        [UniqueTypeOfReceiptName]
        public string Name { get; set; }
    }
}
