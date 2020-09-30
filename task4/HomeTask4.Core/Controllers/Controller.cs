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
        private readonly IUnitOfWork _unitOfWork;
        public Controller(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<List<T>> GetAllItems<T>() where T: BaseEntity
        {
            var list = await _unitOfWork.Repository.ListAsync<T>();
            if (list == null)
            {
                return new List<T>();
            }
            return list;
        }
        public async Task<T> AddItem<T>(T item) where T : BaseEntity
        {
            return await _unitOfWork.Repository.AddAsync<T>(item);
        }
        public async Task<T> GetItemByID<T>(int i) where T: BaseEntity
        {
            return await _unitOfWork.Repository.GetByIdAsync<T>(i);
        }
        public async Task Save()
        {
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
