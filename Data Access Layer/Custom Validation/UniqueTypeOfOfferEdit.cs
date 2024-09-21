using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Custom_Validation
{
    public class UniqueTypeOfOfferEdit:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                // Get the database context from the validation context
                var context = (ShippingDataBase)validationContext.GetService(typeof(ShippingDataBase));

                // Check if an entity with the same name already exists
                var existingEntity = context?.typeOfOffers.FirstOrDefault(s => s.Name == value.ToString());

                // If such an entity exists, ensure it has a different ID (for updates)
                if (existingEntity != null)
                {
                    // Check if the ID of the current entity is different from the found one
                    var currentEntityId = validationContext.ObjectInstance?.GetType().GetProperty("ID")?.GetValue(validationContext.ObjectInstance, null);
                    if (existingEntity.ID != (int?)currentEntityId)
                    {
                        // Validation failed, return error message
                        return new ValidationResult("Name must be unique");
                    }
                }
            }

            // Validation successful
            return ValidationResult.Success;
        }
    }
}
