﻿using Data_Access_Layer.DTO;
using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Interfaces
{
    public interface IGovern:IRepositry<Govern>
    {
        Task<IEnumerable<Govern>> GetGovernWithCities();
        bool IsExist(string govern);
        Task<Govern?> GetByName(string name);
        Task<Govern?> GetByID(int id);


    }
}
