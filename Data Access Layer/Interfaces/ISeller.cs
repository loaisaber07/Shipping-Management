﻿using Data_Access_Layer.DTO.Seller;
using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Interfaces
{
    public interface ISeller : IRepositry<Seller>
    {
        Task<Seller?> DisplayScreenForSeller(string id);
    }
}