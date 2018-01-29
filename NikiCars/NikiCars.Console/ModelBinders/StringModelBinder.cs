using System;
using System.Collections.Generic;
using System.Linq;
using NikiCars.Console.Interfaces;

namespace NikiCars.Console.ModelBinders
{
    public class StringModelBinder : IModelBinder<string>
    {
        public string BindModel(Dictionary<string, string> properties)
        {
            if (properties.Count < 1)
            {
                return string.Empty;
            }

            return properties.First().Value;
        }
    }
}
