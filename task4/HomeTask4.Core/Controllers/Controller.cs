using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HomeTask4.SharedKernel.Interfaces;
using HomeTask4.Core.Entities;
using HomeTask4.SharedKernel;

namespace HomeTask4.Core.Controllers
{
    public class Controller: IController
    {
        private readonly IUnitOfWork unitOfWork;
        public Controller(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public Task<IEnumerable<T>> GetAllItemsAsync<T>() where T: BaseEntity
        {
            return unitOfWork.Repository.GetItemsAsync<T>();
        }
        public Task<T> AddItem<T>(T item) where T : BaseEntity
        {
            return  unitOfWork.Repository.AddItemAsync<T>(item);
        }
        public Task<T> GetItemById<T>(int i) where T: BaseEntity
        {
            return  unitOfWork.Repository.GetByIdAsync<T>(i);
        }
        public async Task SaveAsync()
        {
            await unitOfWork.SaveChangesAsync();
        }
    }
}
