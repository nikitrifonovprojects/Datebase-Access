using System.Collections.Generic;
using NikiCars.Data;
using NikiCars.Data.Models;

namespace NikiCars.Services
{
    public class CarMakeService : BaseService<CarMake>
    {
        public CarMakeService(IRepository<CarMake> repo) 
            : base(repo)
        {
        }
    }
}
