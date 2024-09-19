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
    public class TypeOfReceiptRepository:Repository<TypeOfReceipt>, ITypeOfReceipt
    {
        private readonly ShippingDataBase dataBase;

        public TypeOfReceiptRepository(ShippingDataBase dataBase):base(dataBase) 
        {
            this.dataBase = dataBase;
        }

        public async Task<TypeOfReceipt?> GetReceiptByNameAsync(string Name)
        {
          TypeOfReceipt? type =  await dataBase.typeOfReceipts.AsNoTracking().FirstOrDefaultAsync(t=>t.Name==Name);
         
                return type;
          
        }
    }
}
