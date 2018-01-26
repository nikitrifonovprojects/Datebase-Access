using System.Collections.Generic;

namespace NikiCars.Console.Interfaces
{
    public interface IBindModel<T>
    {
        T ModelBinder(Dictionary<string,string> properties);
    }
}
