using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositry
{
    internal class CityRepository:Repository<City>,ICity
    {
        public CityRepository(ShippingDataBase dataBase) : base(dataBase)
        {

        }
    }
}
