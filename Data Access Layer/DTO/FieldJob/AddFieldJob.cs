using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO.FieldJob
{
    public class AddFieldJob
    {
        [Required]
        public string Name { get; set; }

    }
}
