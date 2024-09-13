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

        public async Task<string> GetNameById(int Id)
        {
            City? city = await dataBase.Cities.AsNoTracking().FirstOrDefaultAsync(s => s.ID == Id);
            if (city is not null)
            {
                return city.Name;
            }
            return string.Empty;
        }

        public async Task<bool> IsExistById(int id)
        {
           City? city= await dataBase.Cities.AsNoTracking().FirstOrDefaultAsync(s => s.ID == id);
            if (city == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> IsExistByName(string Name)
        {
            City? city = await dataBase.Cities.FirstOrDefaultAsync(c => c.Name == Name);
            if (city == null)
            {
                return false;
            }
            return true;
        }
    }
}
