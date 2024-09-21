
using Data_Access_Layer.Entity;
using System.ComponentModel.DataAnnotations;

namespace Data_Access_Layer.DTO
{
    public class UniqueTypeOfChargeAttribute :ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var context = (ShippingDataBase)validationContext.GetService(typeof(ShippingDataBase));

                var entity = context?.typeOfCharges.FirstOrDefault(s => s.Name == value.ToString());
                if (entity != null)
                {
                    return new ValidationResult("Name must be unique ");
                }


            }

            return ValidationResult.Success;

        }
    }
}