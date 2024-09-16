using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositry
{
    public class TypeOfChargeRepository:Repository<TypeOfCharge>,ITypeOfCharge
    {
        private readonly ShippingDataBase dataBase;

        public TypeOfChargeRepository(ShippingDataBase dataBase) : base(dataBase)
        {
            this.dataBase = dataBase;
        }
        public async Task<TypeOfCharge?> GetByName(string name)
        {
            TypeOfCharge? type = await dataBase.typeOfCharges.AsNoTracking().FirstOrDefaultAsync(p => p.Name == name);
            return type;
        }

        public async Task<bool> IsExistByName(string name)
        {
            TypeOfCharge? type = await dataBase.typeOfCharges.AsNoTracking().FirstOrDefaultAsync(p => p.Name == name);
            if (type == null)
            {
                return false;
            }
            return true;
        }
    }
}
