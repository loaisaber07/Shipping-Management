using Data_Access_Layer.DTO.BatchDTO;
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
    public class BranchRepository:Repository<Branch>,IBranch
    {
        private readonly ShippingDataBase dataBase;

        public BranchRepository(ShippingDataBase dataBase) : base(dataBase)
        {
            this.dataBase = dataBase;
        }

        public IEnumerable<BranchDTO> GetAll()
        {
        return    dataBase.branches.Select(s => new BranchDTO
            {
Name = s.Name, 
ID = s.ID,
Date= s.DataAdding
            });
        }

        public BranchDTO? GetByName(string name)
        {
            Branch? b = dataBase.branches.FirstOrDefault(s => s.Name == name);
        if(b is not null) {
                BranchDTO Dto = new BranchDTO
                {
                Name =b.Name , 
                ID= b.ID ,
                Date =b.DataAdding
                };
                return Dto; 
            }
            return null;
                
                }

        public bool IsExist(string name)
        {
        Branch? b=    dataBase.branches
                .AsNoTracking()
                .FirstOrDefault(s => s.Name == name) ; 
        if(b is null) {

                return false;    
            } 
        return true;
        }

        public async Task<bool> IsExistByID(int id)
        {
       Branch? b= await dataBase.branches.FirstOrDefaultAsync(s => s.ID == id);
            if (b is null) { 
            return false;
            } 
            return true;
        }
    }
}
