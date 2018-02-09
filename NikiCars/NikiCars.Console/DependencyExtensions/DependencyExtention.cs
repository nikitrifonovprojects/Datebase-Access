using NikiCars.Command.Interfaces;
using NikiCars.Console.Utility;
using NikiCars.Dependancy;

namespace NikiCars.Command.Framework.DependencyExtensions
{
    public static class DependencyExtention
    {
        public static IDependencyContainer AddConsoleDependencies(this IDependencyContainer container)
        {
            container.RegisterType<IConfig, Config>();

            return container;
        }
    }
}
