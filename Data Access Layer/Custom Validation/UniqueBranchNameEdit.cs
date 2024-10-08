﻿using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Custom_Validation
{
    internal class UniqueBranchNameEdit:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var context = (ShippingDataBase)validationContext.GetService(typeof(ShippingDataBase));
                int? id = context?.branches.FirstOrDefault(s => s.Name == value.ToString())?.ID;
                if (id is null)
                {
                    return ValidationResult.Success;
                }
                var entity = context?.branches.FirstOrDefault(s => s.Name == value.ToString() && s.ID != id);
                if (entity is not null)
                {
                    return new ValidationResult("UserName  must be unique ");
                }


            }

            return ValidationResult.Success;

        }
    }
}
