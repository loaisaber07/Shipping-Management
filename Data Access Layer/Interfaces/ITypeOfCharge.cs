﻿using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Interfaces
{
    public interface ITypeOfCharge:IRepositry<TypeOfCharge>
    {
        Task<bool> IsExistByName(string name);
        Task<TypeOfCharge?> GetByName(string name);
    }
}
