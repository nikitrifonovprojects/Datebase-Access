using System.Collections.Generic;

namespace NikiCars.Console.Interfaces
{
    public interface IModelBinder<T>
    {
        T BindModel(Dictionary<string,string> properties);
    }
}
