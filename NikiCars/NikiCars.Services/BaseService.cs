using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NikiCars.Data;
using NikiCars.Data.Models;

namespace NikiCars.Services
{
    public abstract class BaseService<T> : IService<T> where T : IIdentifiable
    {
        public void Delete(T item)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll(int pageNum, int pageSize)
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public T Save(T item)
        {
            if (item.ID != 0)
            {

            }
            throw new NotImplementedException();
        }
    }
}
