using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using NikiCars.Command.Interfaces;

namespace NikiCars.Command.Framework
{
    public class DefaultModelBinder<T> : IModelBinder<T>
    {
        public T BindModel(Dictionary<string, string> properties)
        {
            Type type = typeof(T);

            if (type.IsPrimitive)
            {
                var result = (T)Activator.CreateInstance(type, true);

                if (properties.Count > 0)
                {
                    result = (T)Convert.ChangeType(properties.First().Value, type);
                }

                return result;
            }
            else if (type.Equals(typeof(string)))
            {
                T result = (T)Convert.ChangeType(string.Empty, type);

                if (properties.Count > 0)
                {
                    result = (T)Convert.ChangeType(properties.First().Value, type);
                }

                return result;
            }
            else if(type.IsClass)
            {
                var result = (T)Activator.CreateInstance(type, true);
                var propertyName = result.GetType().GetProperties().Select(x => x.Name);
                var propertyType = result.GetType().GetProperties().Select(x => x.PropertyType.Name);

                foreach (var item in propertyName)
                {
                    if (properties.ContainsKey(item))
                    {
                        var property = result.GetType().GetProperty(item);
                        property.SetValue(result, JsonConvert.DeserializeObject(properties[item], property.PropertyType));
                    }
                }

                return result;
            }

            throw new NotSupportedException("Type is not supported");
        }
    }
}
