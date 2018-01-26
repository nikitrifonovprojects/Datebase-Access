using System.Collections.Generic;
using NikiCars.Console.Interfaces;
using NikiCars.Data.Models;

namespace NikiCars.Console.Commands
{
    public class BindModelCarCoupe : IBindModel<CarCoupe>
    {
        public CarCoupe ModelBinder(Dictionary<string, string> properties)
        {
            return new CarCoupe()
            {
                Name = properties["Name"]
            };
        }
    }
}
