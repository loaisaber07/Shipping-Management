using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO
{
    public class GovernDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }  
        public bool Status { get; set; }
    public    List<CityDTO>? cities { get; set; }
    }
}
