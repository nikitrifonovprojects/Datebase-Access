using System.Collections.Generic;
using NikiCars.Console.Interfaces;
using NikiCars.Data.Models;

namespace NikiCars.Console.Commands
{
    public class BindModelCarMake : IBindModel<CarMake>
    {
        public CarMake ModelBinder(Dictionary<string, string> properties)
        {
            return new CarMake()
            {
                Name = properties["Name"],
                Country = properties["Country"]
            };
        }
    }
}
