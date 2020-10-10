using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTask4.SharedKernel.Interfaces
{
    public interface IRepository
    {
        Task<T> GetByIdAsync<T>(int id) where T : BaseEntity;
        Task<IEnumerable<T>> GetItemsAsync<T>() where T : BaseEntity;
        Task<T> AddItemAsync<T>(T entity) where T : BaseEntity;
        T Update<T>(T entity) where T : BaseEntity;
        T DeleteItem<T>(T entity) where T : BaseEntity;
    }
}
