using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Interfaces
{
    public interface IPrivilege:IRepositry<Privilege>
    {
        Task<bool> IsExsitsByName(string Name);
        Task<bool> IsExsitsById(int Id);
        Task<string> GetNameById(int Id);
    }
}
