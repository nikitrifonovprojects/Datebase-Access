using System;

namespace NikiCars.Command.Interfaces
{
    public interface IModelBinder
    {
        dynamic BindModel(string data, Type type);
    }
}
