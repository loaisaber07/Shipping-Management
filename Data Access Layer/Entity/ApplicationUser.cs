using Data_Access_Layer.Custom_Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entity
{
    public  class ApplicationUser : IdentityUser
    {
        [UniqueUserName]
        [Required]
        public string UserName { get; set; }
        [StringLength(maximumLength:100)]
        public string? Address { get; set; }

    }
}
