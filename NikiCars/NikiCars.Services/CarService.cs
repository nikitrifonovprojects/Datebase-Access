using System.Collections.Generic;
using NikiCars.Data.Interfaces;
using NikiCars.Data.Models;
using NikiCars.Data.Models.Includes;
using NikiCars.Search;
using NikiCars.Search.Interfaces;
using NikiCars.Services.Interfaces;

namespace NikiCars.Services
{
    public class CarService : BaseService<Car>, ICarService
    {
        private ICarRepository carRepository;

        public CarService(ICarRepository repo) 
            : base(repo)
        {
            this.carRepository = repo;
        }

        public List<Car> GetAll(List<IEntitySearch<Car>> search, List<IEntityOrderBy<Car>> order, Pagination pagination = null, List<CarIncludes> includes = null)
        {
            return this.carRepository.GetAll(search, order, pagination, includes);
        }

        public Car GetById(int id, List<CarIncludes> includes = null)
        {
            return this.carRepository.GetById(id, includes);
        }

        public override void Dispose()
        {
            base.Dispose();
            this.carRepository.Dispose();
        }
    }
}
