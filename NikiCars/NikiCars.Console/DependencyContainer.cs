using NikiCars.Data;
using NikiCars.Data.Models;
using NikiCars.Services;
using Unity;

namespace NikiCars.Console
{
    public static class DependencyContainer
    {
        private static IUnityContainer container;

        public static void RegisterDependencies()
        {
            container = new UnityContainer();

            container.RegisterType(typeof(IService<>), typeof(BaseService<>));
        }

        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}
