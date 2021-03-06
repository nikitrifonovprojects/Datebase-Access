﻿using System.Collections.Generic;
using NikiCars.Data.Models;
using NikiCars.Data.Models.Includes;
using NikiCars.Search;
using NikiCars.Search.Interfaces;

namespace NikiCars.Data.Interfaces
{
    public interface ICarRepository : IRepository<Car>
    {
        List<Car> GetAll(List<IEntitySearch<Car>> search, List<IEntityOrderBy<Car>> order, Pagination pagination = null, List<CarIncludes> includes = null);

        Car GetById(int id, List<CarIncludes> includes = null);
    }
}
