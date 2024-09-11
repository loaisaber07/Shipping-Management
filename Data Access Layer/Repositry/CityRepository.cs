using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositry
{
    public class CityRepository:Repository<City>,ICity
    {
        private readonly ShippingDataBase dataBase;

        public CityRepository(ShippingDataBase dataBase) : base(dataBase)
        {
            this.dataBase = dataBase;
        }

        public  async Task BulkInsert(IEnumerable<City> cities)
        {
        await    dataBase.Cities.AddRangeAsync(cities);
         await   dataBase.SaveChangesAsync();
        }
    }
}
