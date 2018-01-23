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
            container.RegisterType<IRepository<CarCoupe>, CarCoupeRepository>();
            container.RegisterType<IService<CarCoupe>, CarCoupeService>();

            container.RegisterType<IRepository<CarMake>, CarMakeRepository>();
            container.RegisterType<IService<CarMake>, CarMakeService>();

            container.RegisterType<IRepository<CarModel>, CarModelRepository>();
            container.RegisterType<IService<CarModel>, CarModelService>();
        }

        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}
