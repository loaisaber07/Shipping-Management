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
    public class Repository<T> : IRepositry<T> where T : class
    {
        private readonly ShippingDataBase db;
        private readonly  DbSet<T> dbSet;

        public Repository(ShippingDataBase db)
        {
            this.db = db;
            dbSet=db.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
           return await dbSet.ToListAsync();
        }
        public async Task<T?> GetAsyncById(int id)
        {
            T? entity = await dbSet.FindAsync(id);
            return entity;
        }
        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            
        }

        public async Task UpdateAsync(T entity)
        {
            
            dbSet.Update(entity);
            
            
        }
        public async Task DeleteAsync(int id)
        {
            T? entity = await dbSet.FindAsync(id);
            if (entity != null)
            {
                dbSet.Remove(entity);
            }
            
        }



        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

    }
}
