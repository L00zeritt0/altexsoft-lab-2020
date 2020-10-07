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

        public T Add<T>(T entity) where T : BaseEntity
        {
            return context.Set<T>().Add(entity).Entity;
        }
        public T Delete<T>(T entity) where T : BaseEntity
        {
            return context.Set<T>().Remove(entity).Entity;
        }
        public T GetById<T>(int id) where T : BaseEntity
        {
            return context.Set<T>().Find(id);
        }
        public Task<IEnumerable<T>> GetItemsAsync<T>() where T : BaseEntity
        {
            return Task.Run(() => { return context.Set<T>().AsEnumerable(); });
        }
        public T Update<T>(T entity) where T : BaseEntity
        {
            return context.Set<T>().Update(entity).Entity;
        }
    }
}
