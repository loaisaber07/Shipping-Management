using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Interfaces
{
    public interface ITypeOfPayment:IRepositry<TypeOfPayment>
    {
        Task<bool> IsExistByName(string name);
        Task<TypeOfPayment?> GetByName(string name);
    }
}
