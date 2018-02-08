using System;
using System.Collections.Generic;

namespace NikiCars.Data
{
    public interface IRepository<T> : IDisposable
    {
        T GetByID(int id);

        List<T> GetAll(int pageNumber, int pageSize);

        T Create(T item);

        T Update(T item);

        void Delete(T item);

        int Count();
    }
}
