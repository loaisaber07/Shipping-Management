using Data_Access_Layer.Custom_Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO.Agent
{
    public class GetAgentsToAsigenOrderDTO
    {
        public string ID { get; set; }
        public string UserName { get; set; }
        public string Govern { get; set; }
    }
}
