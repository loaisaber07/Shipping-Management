﻿using Data_Access_Layer.Custom_Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO.Privilege
{
    public class EditPrivilegeDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
