using Data_Access_Layer.Custom_Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO.BatchDTO
{
    public class EditBranchDTO
    {
        [Required]
        public int ID { get; set; }
        [UniqueBranchNameEdit]
        public string Name { get; set; }
        public bool Status { get; set; }
    }
}
