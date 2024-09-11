using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Data_Access_Layer.DTO;

namespace Data_Access_Layer.Repositry
{
    public class GovernRepository:Repository<Govern>,IGovern
    {
        private readonly ShippingDataBase dataBase;

        public GovernRepository(ShippingDataBase dataBase):base(dataBase) 
        {
            this.dataBase = dataBase;
        }

        public Govern GetByName(string name)
        {
       Govern? g=     dataBase.governs.FirstOrDefault(s => s.Name == name);
            return g;

        
        }

        public async Task<IEnumerable<GovernDTO>> GetGovernWithCities()
        {
        List<Govern> g=  await dataBase.governs.Include(c => c.Cities).ToListAsync();
        return    g.Select(c => new GovernDTO
            {
                ID = c.ID,
                Name = c.Name,
                cities = c.Cities.Select(city => new CityDTO {
                ID= city.ID,
                Name= city.Name,
                }).ToList()

            });
        }

        public Govern? GetWithID(int id)
        {
        return dataBase.governs.AsNoTracking().FirstOrDefault(s => s.ID == id);
        }

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
