using System.Threading.Tasks;
using HomeTask4.SharedKernel.Interfaces;

namespace HomeTask4.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private RecipeBookDbContext context;

        public IRepository Repository { get; }

        public UnitOfWork(RecipeBookDbContext context, IRepository repository)
        {
            this.context = context;
            Repository = repository;
        }

        public Task SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}
