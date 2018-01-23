using System.Collections.Generic;
using NikiCars.Data;
using NikiCars.Data.Models;

namespace NikiCars.Services
{
    public class CarCoupeService : BaseService<CarCoupe>
    {
        public CarCoupeService(IRepository<CarCoupe> repo) 
            : base(repo)
        {
        }
    }
}
