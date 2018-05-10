using System.Collections.Generic;
using NikiCars.Data.Interfaces;
using NikiCars.Data.Models;
using NikiCars.Search;
using NikiCars.Search.Interfaces;
using NikiCars.Services.Interfaces;

namespace NikiCars.Services
{
    public class BaseService<T> : IService<T> where T : IIdentifiable
    {
        private IRepository<T> repository;

        public BaseService(IRepository<T> repo)
        {
            this.repository = repo;
        }

        public bool Delete(T item)
        {
            return this.repository.Delete(item);
        }

        public List<T> GetAll(Pagination pagination)
        {
            return this.repository.GetAll(pagination);
        }

        public List<T> GetAll(List<IEntitySearch<T>> search)
        {
            return this.repository.GetAll(search);
        }

        public List<T> GetAll()
        {
            return this.repository.GetAll();
        }

        public List<T> GetAll(List<IEntityOrderBy<T>> order)
        {
            return this.repository.GetAll(order);
        }

        public List<T> GetAll(List<IEntityOrderBy<T>> order, Pagination pagination)
        {
            return this.repository.GetAll(order, pagination);
        }

        public List<T> GetAll(List<IEntitySearch<T>> search, List<IEntityOrderBy<T>> order, Pagination pagination)
        {
            return this.repository.GetAll(search, order, pagination);
        }

        public List<T> GetAll(List<IEntitySearch<T>> search, List<IEntityOrderBy<T>> order)
        {
            return this.repository.GetAll(search, order);
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

        private T Update(T item)
        {
            this.repository.Update(item);
            return item;
        }

        private T Create(T item)
        {
            return this.repository.Create(item);
        }

        public int Count()
        {
            return this.repository.Count();
        }

        public virtual void Dispose()
        {
            this.repository.Dispose();
        }
    }
}
