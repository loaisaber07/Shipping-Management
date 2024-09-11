using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public bool BulkRemove(IEnumerable<City> cities)
        {
            try
            {
                dataBase.Cities.RemoveRange(cities);
                return true; 
            }
            catch {
                return false;
            }

        }

        public async Task<IEnumerable<City>> BulkSelect(int governID)
        {
    return await  dataBase.Cities.Where(s => s.GovernID == governID).ToListAsync();
        }
    }
}
