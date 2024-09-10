﻿using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositry
{
    public class AgentRepository:Repository<Agent>,IAgent
    {
        public AgentRepository(ShippingDataBase dataBase) : base(dataBase)
        {

        }
    }
}
