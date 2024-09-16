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
    public class TypeOfPaymentRepository:Repository<TypeOfPayment>,ITypeOfPayment
    {
        private readonly ShippingDataBase dataBase;

        public TypeOfPaymentRepository(ShippingDataBase dataBase):base(dataBase)
        {
            this.dataBase = dataBase;
        }

        public async Task<TypeOfPayment?> GetByName(string name)
        {
            TypeOfPayment? type=  await dataBase.typeOfPayments.AsNoTracking().FirstOrDefaultAsync(p => p.Name == name);
            return type;
        }

        public async Task<bool> IsExistByName(string name)
        {
           TypeOfPayment? type = await dataBase.typeOfPayments.AsNoTracking().FirstOrDefaultAsync(p=>p.Name==name);
            if (type == null)
            {
                return false;
            }
            return true;
        }
    }
}
