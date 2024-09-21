﻿using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Interfaces
{
    public interface IOrder:IRepositry<Order>
    {
        Task<Order?> GetById(int id); 
        IQueryable<Order> GetAll(); 
        bool ISEXIST(int id);
    }
}
