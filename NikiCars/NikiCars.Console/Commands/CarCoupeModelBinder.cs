using System;
using System.Collections.Generic;
using NikiCars.Console.Interfaces;
using NikiCars.Data.Models;

namespace NikiCars.Console.Commands
{
    public class CarCoupeModelBinder : IModelBinder<CarCoupe>
    {
        public CarCoupe BindModel(Dictionary<string, string> properties)
        {
            CarCoupe model = new CarCoupe();
            
            if (properties.ContainsKey("Name"))
            {
                model.Name = properties["Name"];
            }

            if (properties.ContainsKey("ID"))
            {
                model.ID = Convert.ToInt32(properties["ID"]);
            }

            return model;
        }
    }
}
