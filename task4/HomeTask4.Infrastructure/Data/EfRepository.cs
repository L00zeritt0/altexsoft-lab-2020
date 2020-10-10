using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeTask4.SharedKernel;
using HomeTask4.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HomeTask4.Infrastructure.Data
{
    public class EfRepository : IRepository
    {
        private RecipeBookDbContext context;
        public EfRepository(RecipeBookDbContext context)
        {
            this.context = context;
        }

        public async Task<T> AddItemAsync<T>(T entity) where T : BaseEntity
        {
            return (await context.Set<T>().AddAsync(entity)).Entity;
        }
        public T DeleteItem<T>(T entity) where T : BaseEntity
        {
            return context.Set<T>().Remove(entity).Entity;
        }
        public async Task<T> GetByIdAsync<T>(int id) where T : BaseEntity
        {
            return await context.Set<T>().FindAsync(id).AsTask();
        }
        public async Task<IEnumerable<T>> GetItemsAsync<T>() where T : BaseEntity
        {
            return await context.Set<T>().ToListAsync();
        }
        public T Update<T>(T entity) where T : BaseEntity
        {
            return context.Set<T>().Update(entity).Entity;
        }
    }
}
