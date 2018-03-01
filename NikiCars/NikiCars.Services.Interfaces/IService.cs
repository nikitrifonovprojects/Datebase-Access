using System;
using System.Collections.Generic;
using NikiCars.Data.Models;
using NikiCars.Search;
using NikiCars.Search.Interfaces;

namespace NikiCars.Services.Interfaces
{
    public interface IService<T> : IDisposable where T : IIdentifiable
    {
        T Save(T item);

        bool Delete(T item);

        List<T> GetAll(Pagination pagination);

        List<T> GetAll(List<IEntityOrderBy<T>> order);

        List<T> GetAll(List<IEntityOrderBy<T>> order, Pagination pagination);

        List<T> GetAll();

        List<T> GetAll(List<IEntitySearch<T>> search);

        List<T> GetAll(List<IEntitySearch<T>> search, List<IEntityOrderBy<T>> order, Pagination pagination);

        T GetById(int id);

        int Count();
    }
}
