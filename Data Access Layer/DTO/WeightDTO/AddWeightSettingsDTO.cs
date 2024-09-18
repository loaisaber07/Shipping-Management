using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO.WeightDTO
{
    public class AddWeightSettingsDTO
    {
        public int DefaultWeight { get; set; } = 100;
        public int AdditionalWeight { get; set; } = 0;
    }
}
