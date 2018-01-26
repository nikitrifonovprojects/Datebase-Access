using System.Linq;
using System.Reflection;
using NikiCars.Console.Commands;
using NikiCars.Console.Input;
using NikiCars.Console.Interfaces;
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
            
            var assembly = Assembly.GetAssembly(typeof(IRepository<>));
            var types = assembly.GetExportedTypes().Where(t => !t.IsAbstract && t.IsClass && t.GetInterfaces().Any(c => c.IsGenericType && typeof(IRepository<>).IsAssignableFrom(c.GetGenericTypeDefinition())));

            foreach (var type in types)
            {
                var inheritedFrom = type.GetInterfaces().First(x => x.IsGenericType && typeof(IRepository<>).IsAssignableFrom(x.GetGenericTypeDefinition()));
                
                container.RegisterType(inheritedFrom, type);
            }

            container.RegisterType<IParser, Parser>();
            container.RegisterType<Invoker, Invoker>();
            container.RegisterType<IModelBinder<CarCoupe>, CarCoupeModelBinder>();
            container.RegisterType<IModelBinder<CarMake>, CarMakeModelBinder>();
        }

        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}
