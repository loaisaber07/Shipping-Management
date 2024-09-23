﻿using Data_Access_Layer.Custom_Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO
{
    public class AddOrderStatusDTO
    {
        [UniqueOrderStatus]
        [AllowedValues("New", "Waiting",
            "AssignedToAgent", "Delivered",
            "UnReachable", "Postponed", 
            "PartiallyDelivered",
            "Canceled", "RejectedWithPayment",
            "RejectWithPartialPayment",
            "RejectedAndNotPaid")]
        public string Name { get; set; }
    }
}
