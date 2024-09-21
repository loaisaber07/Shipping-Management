using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Custom_Validation
{
    internal class UniqueTyprOfChargeNameEdit:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                // Get the database context from the validation context
                var context = (ShippingDataBase)validationContext.GetService(typeof(ShippingDataBase));

                // Check if the value exists in the typeOfPayments table
                var existingEntity = context?.typeOfCharges.FirstOrDefault(s => s.Name == value.ToString());

                // Retrieve the current entity's ID (to handle updates)
                var currentEntityId = validationContext.ObjectInstance?.GetType().GetProperty("ID")?.GetValue(validationContext.ObjectInstance, null);

                // If the name already exists and belongs to a different entity, return a validation error
                if (existingEntity != null && existingEntity.ID != (int?)currentEntityId)
                {
                    return new ValidationResult("Name must be unique");
                }
            }

            // Validation successful
            return ValidationResult.Success;
        }
    }
}
