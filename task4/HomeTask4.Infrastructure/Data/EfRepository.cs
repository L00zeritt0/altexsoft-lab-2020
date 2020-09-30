using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeTask4.SharedKernel;
using HomeTask4.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeTask4.Infrastructure.Data
{
    public class EfRepository : IRepository
    {
        RecipeBookDbContext context;
        public EfRepository(DbContext context)
        {
            this.context = (RecipeBookDbContext)context;
        }
        public async Task<T> AddAsync<T>(T entity) where T : BaseEntity
        {
            return await Task.Run<T>(() =>
            {
                return context.Set<T>().Add(entity).Entity;
            }).ConfigureAwait(false);
        }
        public async Task<T> DeleteAsync<T>(T entity) where T : BaseEntity
        {
            return await Task.Run(() => context.Set<T>().Remove(entity).Entity);
        }
        public async Task<T> GetByIdAsync<T>(int id) where T : BaseEntity
        {
            return await Task.Run(() => { return context.Set<T>().Find(id); });
        }
        public async Task<List<T>> ListAsync<T>() where T : BaseEntity
        {
            return await Task.Run(() => { return context.Set<T>().ToList(); }).ConfigureAwait(false);
        }
        public async Task<T> UpdateAsync<T>(T entity) where T : BaseEntity
        {
            return await Task.Run(() => context.Set<T>().Update(entity).Entity);
        }
    }
}
