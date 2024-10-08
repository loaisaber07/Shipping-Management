﻿using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Interfaces
{
    public interface ICity:IRepositry<City>
    {
        Task BulkInsert(IEnumerable<City> cities);
        bool BulkRemove(IEnumerable<City> cities);
        Task<IEnumerable<City>> BulkSelect(int governID);
        Task<string>GetNameById(int Id);
        Task<bool> IsExistById(int id);
        Task<bool> IsExistByName(string Name);
    }
}
