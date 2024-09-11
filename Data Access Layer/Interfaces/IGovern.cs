using Data_Access_Layer.DTO;
using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Interfaces
{
    public interface IGovern:IRepositry<Govern>
    {
        Task<IEnumerable<GovernDTO>> GetGovernWithCities();
        bool IsExist(string govern);
        Govern GetByName(string name);
        Govern? GetWithID(int id); 


    }
}
