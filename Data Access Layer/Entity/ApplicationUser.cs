using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entity
{
    public  class ApplicationUser : IdentityUser
    { 
        
        public string? Address { get; set; }

    }
}
