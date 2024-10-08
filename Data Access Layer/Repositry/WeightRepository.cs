﻿using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositry
{
    public class WeightRepository:Repository<Weight>,IWeight
    {
        private readonly ShippingDataBase dataBase;

        public WeightRepository(ShippingDataBase dataBase) : base(dataBase)
        {
            this.dataBase = dataBase;
        }

        public Weight? GetDefaultWeight(out bool IsExist)
        {
            Weight? weight =dataBase
                           .weights
                           .FirstOrDefault();
            if (weight is null) {
                IsExist = false;
                return null; 
            }  
            IsExist = true;
            return weight;
            
        }
    }
}
