using Data_Access_Layer.Custom_Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO.GovernAndCity
{
    public class EditGovernDTO
    {
        public int ID { get; set; }
        [UniqueGovernNameEdit]
        public string Name { get; set; }
        public bool Status { get; set; }
    }
}
