using System;
using System.Collections.Generic;
using NikiCars.Console.Interfaces;
using NikiCars.Data.Models;

namespace NikiCars.Console.Commands
{
    public class CarMakeModelBinder : IModelBinder<CarMake>
    {
        public CarMake BindModel(Dictionary<string, string> properties)
        {
            CarMake model = new CarMake();

            if (properties.ContainsKey("Name"))
            {
                model.Name = properties["Name"];
            }

            if (properties.ContainsKey("Country"))
            {
                model.Country = properties["Country"];
            }

            if (properties.ContainsKey("ID"))
            {
                model.ID = Convert.ToInt32(properties["ID"]);
            }

            return model;
        }
    }
}
