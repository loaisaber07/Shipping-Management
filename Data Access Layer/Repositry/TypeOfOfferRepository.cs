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
    public class TypeOfOfferRepository:Repository<TypeOfOffer>,ITypeOfOffer
    {
        private readonly ShippingDataBase dataBase;

        public TypeOfOfferRepository(ShippingDataBase dataBase) : base(dataBase)
        {
            this.dataBase = dataBase;
        }

        public async Task<TypeOfOffer?> getByNameAsync(string name)
        {
            TypeOfOffer? type = await dataBase.typeOfOffers.AsNoTracking().FirstOrDefaultAsync(t=>t.Name==name);
            return type;
        }
    }
}
