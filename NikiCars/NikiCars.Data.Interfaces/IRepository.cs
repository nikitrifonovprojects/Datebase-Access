using System;
using System.Collections.Generic;
using NikiCars.Search;
using NikiCars.Search.Interfaces;

namespace NikiCars.Data.Interfaces
{
    public interface IRepository<T> : IDisposable
    {
        T GetByID(int id);

        List<T> GetAll(Pagination pagination);

        List<T> GetAll();

        List<T> GetAll(List<IEntitySearch<T>> search);

        List<T> GetAll(List<IEntitySearch<T>> search, List<IEntityOrderBy<T>> order, Pagination pagination);

        T Create(T item);

        T Update(T item);

        bool Delete(T item);

        int Count();
    }
}
