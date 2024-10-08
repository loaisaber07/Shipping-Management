﻿using Data_Access_Layer.DTO.FieldJob;
using Data_Access_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Interfaces
{
    public interface IFieldJob :IRepositry<FieldJob>
    {
        IEnumerable<FieldJobDTO> GetAllFieldWithPrivilege();
        bool IsExist(string name);
        FieldJob? GetByName(string name);
        Task<bool> IsExistByIdAsync(int id);
        Task<FieldJob?> GetFieldJobById(int id); 


    }
}
