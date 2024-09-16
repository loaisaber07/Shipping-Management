using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Data_Access_Layer.DTO;

namespace Data_Access_Layer.Repositry
{
    public class GovernRepository : Repository<Govern>, IGovern
    {
        private readonly ShippingDataBase dataBase;

        public GovernRepository(ShippingDataBase dataBase):base(dataBase) 
        {
            this.dataBase = dataBase;
        }

        public async Task<Govern?> GetByName(string name)
        {
            return await dataBase.governs.FirstOrDefaultAsync(s => s.Name == name);
            
        }

        public async Task<IEnumerable<Govern>> GetGovernWithCities()
        {
            return  await dataBase.governs.Include(c => c.Cities).ToListAsync();
        }

        public async Task<Govern?> GetByID(int id) => await dataBase.governs.Include(c => c.Cities).AsNoTracking().FirstOrDefaultAsync(s => s.ID == id);
        public bool IsExist(string govern)
        {
            Govern? result = dataBase.governs.FirstOrDefault(s => s.Name == govern);
            if (result is not null) {
                return true; 
            }
            return false;
        
        }


    }
}
