using System;
using Newtonsoft.Json;
using NikiCars.Command.Interfaces;

namespace NikiCars.Command.Framework
{
    public class DefaultModelBinder : IModelBinder
    {
        public dynamic BindModel(string data, Type type)
        {
            dynamic result = JsonConvert.DeserializeObject(data, type);

            return result;
        }
    }
}
