using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO
{
    public class GetCityDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int NormalCharge { get; set; }
        public int PickUpCharge { get; set; }
        public int GovernID { get; set; }

    }
}
