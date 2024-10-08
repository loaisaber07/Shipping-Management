﻿using Data_Access_Layer.Custom_Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO.Agent
{
    public class GetAgentDTO
    {
        public string ID { get; set; }
        public string Name { get; set; }       
        public string Email { get; set; }       
        public string Phone { get; set; }        
        public string Branch { get; set; }        
        public int ThePrecentageOfCompanyFromOffer { get; set; }
        public string Govern { get; set; }
        public string TypeOfOffer{ get; set; }
        public string Address  { get; set; }
    }
}
