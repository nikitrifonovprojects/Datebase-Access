using System.Collections.Generic;
using NikiCars.Data.Models;
using NikiCars.Data;

namespace NikiCars.Services
{
    public abstract class BaseService<T> : IService<T> where T : IIdentifiable
    {
        protected IRepository<T> repository;

        public BaseService(IRepository<T> repo)
        {
            this.repository = repo;
        }

        public void Delete(T item)
        {
            this.repository.Delete(item);
        }

        public List<T> GetAll(int pageNum, int pageSize)
        {
            return this.repository.GetAll(pageNum, pageSize);
        }

        public T GetById(int id)
        {
            return this.repository.GetByID(id);
        }

        public T Save(T item)
        {
            if (item.ID == 0)
            {
                return Create(item);
            }

            return Update(item);
        }

        protected T Update(T item)
        {
            this.repository.Update(item);
            return item;
        }

        protected T Create(T item)
        {
            return this.repository.Create(item);
        }
        
        public void Dispose()
        {
            this.repository.Dispose();
        }
    }
}
