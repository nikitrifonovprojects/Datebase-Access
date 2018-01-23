using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NikiCars.Data;
using NikiCars.Data.Models;

namespace NikiCars.Services
{
    public class CarModelService : BaseService<CarModel>
    {
        public CarModelService(IRepository<CarModel> repo) 
            : base(repo)
        {
        }
    }
}
