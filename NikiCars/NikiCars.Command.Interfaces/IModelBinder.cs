using System.Collections.Generic;

namespace NikiCars.Command.Interfaces
{
    public interface IModelBinder<T>
    {
        T BindModel(Dictionary<string,string> properties);
    }
}
