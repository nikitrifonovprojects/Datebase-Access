using System;
using System.Collections.Generic;

namespace NikiCars.Data
{
    interface IRepository<T> : IDisposable
    {
        T GetByID(int id);

        List<T> GetAll(int pageNumber, int pageSize);

        T Create(T item);

        void Update(T item);

        void Delete(T item);
    }
}
