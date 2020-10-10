using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeTask4.SharedKernel.Interfaces
{
    public interface IController
    { 
        Task<IEnumerable<T>> GetAllItemsAsync<T>() where T : BaseEntity;
        Task<T> AddItem<T>(T item) where T : BaseEntity;
        Task<T> GetItemById<T>(int i) where T : BaseEntity;
        Task SaveAsync();
    }
}
