﻿using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Custom_Validation
{
    internal class UniquePrivilegeName :ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var context = (ShippingDataBase)validationContext.GetService(typeof(ShippingDataBase));
                var entity = context.privileges.FirstOrDefault(s => s.Name == value.ToString());
                if (entity != null)
                {
                    return new ValidationResult("Privilege Name must be unique ");
                }


            }

            return ValidationResult.Success;

        }
    }
}
