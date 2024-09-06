using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entity
{
    public  class Seller :IdentityUser
    { 

        [Required]
        public string StoreName { get; set; }
        public int PickUp { get; set; } = 0;
        public int? ValueOfRejectedOrder { get; set; } 
    }
}
