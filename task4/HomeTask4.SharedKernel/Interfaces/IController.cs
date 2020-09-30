using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeTask4.SharedKernel.Interfaces
{
    public interface IController
    {
        Task<List<T>> GetAllItems<T>() where T : BaseEntity;
        Task<T> AddItem<T>(T item) where T : BaseEntity;
        Task<T> GetItemByID<T>(int i) where T : BaseEntity;
        Task Save();
    }
}
