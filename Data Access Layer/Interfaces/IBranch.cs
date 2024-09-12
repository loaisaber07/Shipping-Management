using Data_Access_Layer.DTO.BatchDTO;
using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Interfaces
{
    public interface IBranch:IRepositry<Branch>
    {
        bool IsExist(string name);
        BranchDTO?  GetByName(string name);
        IEnumerable<BranchDTO> GetAll();
        Task<bool> IsExistByID(int id); 
    }
}
