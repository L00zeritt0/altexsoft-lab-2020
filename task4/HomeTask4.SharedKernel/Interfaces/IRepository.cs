using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTask4.SharedKernel.Interfaces
{
    public interface IRepository
    {
        T GetById<T>(int id) where T : BaseEntity;
        Task<IEnumerable<T>> GetItemsAsync<T>() where T : BaseEntity;
        T Add<T>(T entity) where T : BaseEntity;
        T Update<T>(T entity) where T : BaseEntity;
        T Delete<T>(T entity) where T : BaseEntity;
    }
}
