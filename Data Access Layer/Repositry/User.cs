using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositry
{
    public class User:Repository<User>,IUser
    {
        private readonly ShippingDataBase db;

        public User(ShippingDataBase db):base(db)
        {
            this.db = db;
        }
    }
}
