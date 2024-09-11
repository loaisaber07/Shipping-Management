using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO.GovernAndCity
{
    public class AddGovernWithCities
    {
        [Required]
        public string Name { get; set;}
      public   List<AddCity>? cities { get; set; }
    }
}
