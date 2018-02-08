using System;
using System.Collections.Generic;
using NikiCars.Data.Models;

namespace NikiCars.Services.Interfaces
{
    public interface IService<T> : IDisposable where T : IIdentifiable
    {
        T Save(T item);

        void Delete(T item);

        List<T> GetAll(int pageNum, int pageSize);

        T GetById(int id);

        int Count();
    }
}
