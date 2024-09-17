using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Custom_Validation
{
    public class UniqueTypeOfCharge
    {
        public class UniqueCityName : ValidationAttribute
        {
            protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            {
                if (value != null)
                {
                    var context = (ShippingDataBase)validationContext.GetService(typeof(ShippingDataBase));
                    var entity = context.typeOfCharges.FirstOrDefault(s => s.Name == value.ToString());
                    if (entity != null)
                    {
                        return new ValidationResult("type Of Charge Name must be unique ");
                    }
                }

                return ValidationResult.Success;

            }
        }
    }
}
