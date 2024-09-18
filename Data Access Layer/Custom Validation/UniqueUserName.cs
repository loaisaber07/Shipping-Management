using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Custom_Validation
{
    internal class UniqueUserName : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var context = (ShippingDataBase)validationContext.GetService(typeof(ShippingDataBase));
                var entity = context.Users.FirstOrDefault(s => s.UserName == value.ToString());
                if (entity != null)
                {
                    return new ValidationResult("User Name must be unique ");
                }


            }

            return ValidationResult.Success;

        }
    }
}
