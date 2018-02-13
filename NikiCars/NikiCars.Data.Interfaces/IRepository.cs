using System;
using System.Collections.Generic;

namespace NikiCars.Data.Interfaces
{
    public interface IRepository<T> : IDisposable
    {
        T GetByID(int id);

        List<T> GetAll(int pageNumber, int pageSize);

        List<T> GetAll();

        T Create(T item);

        T Update(T item);

        bool Delete(T item);

        int Count();
    }
}
