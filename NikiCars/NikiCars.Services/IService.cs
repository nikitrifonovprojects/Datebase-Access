using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NikiCars.Data.Models;

namespace NikiCars.Services
{
    public interface IService<T> where T : IIdentifiable
    {
        T Save(T item);

        void Delete(T item);

        List<T> GetAll(int pageNum, int pageSize);

        T GetById(int id);
    }
}
