using System;
using System.Collections.Generic;
using System.Linq;
using NikiCars.Console.Interfaces;

namespace NikiCars.Console.ModelBinders
{
    public class DefaultModelBinder<T> :IModelBinder<T>
    {
        public T BindModel(Dictionary<string, string> properties)
        {
            var listOfProperties = new Dictionary<string, string>();
            Type type = typeof(T);
            var result = (T)Activator.CreateInstance(type);
            var propertyName = result.GetType().GetProperties().Select(x => x.Name);
            var propertyType = result.GetType().GetProperties().Select(x => x.PropertyType.Name);

            foreach (var item in propertyName)
            {
                if (properties.ContainsKey(item))
                {
                    var temp = result.GetType().GetProperty(item);
                    temp.SetValue(result, Convert.ChangeType(properties[item], temp.PropertyType));
                }
            }

            return result;
        }
    }
}
