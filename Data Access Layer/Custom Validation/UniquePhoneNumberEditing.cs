using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Custom_Validation
{
    internal class UniquePhoneNumberEditing :ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var context = (ShippingDataBase)validationContext.GetService(typeof(ShippingDataBase));
                string? id= context?.Users.FirstOrDefault(s=>s.PhoneNumber == value.ToString())?.Id;
                if (id is null) {
                    return ValidationResult.Success;
                }
                var entity = context?.Users.FirstOrDefault(s => s.PhoneNumber == value.ToString() && s.Id != id);
                if (entity is not null)
                {
                    return new ValidationResult("Phone Number must be unique ");
                }


            }

            return ValidationResult.Success;

        }
    }
}
