using System;
using System.Linq;
using System.Reflection;
using NikiCars.Authentication;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Console.Input;
using NikiCars.Console.ModelBinders;
using NikiCars.Console.Routing;
using NikiCars.Console.Utility;
using NikiCars.Data;
using NikiCars.Data.Interfaces;
using NikiCars.Services;
using NikiCars.Services.Interfaces;
using Unity;
using Unity.Resolution;

namespace NikiCars.Console
{
    public static class DependencyContainer
    {
        private static IUnityContainer container;

        public static void RegisterDependencies()
        {
            container = new UnityContainer();
            container.RegisterType<IConfig, Config>();
            container.RegisterType<IAuthenticationManager, AuthenticationManager>();

            container.RegisterType(typeof(IService<>), typeof(BaseService<>));
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<ICryptographyService, CryptographyService>();

            container.RegisterType<IUserRepository, UserRepository>();
            
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetExportedTypes())
                .Where(t => !t.IsAbstract && t.IsClass && t.GetInterfaces()
                .Any(c => c.IsGenericType && typeof(IRepository<>)
                .IsAssignableFrom(c.GetGenericTypeDefinition())));

            foreach (var type in types)
            {
                var inheritedFrom = type.GetInterfaces()
                    .First(x => x.IsGenericType && typeof(IRepository<>).IsAssignableFrom(x.GetGenericTypeDefinition()));
                
                container.RegisterType(inheritedFrom, type);
            }

            container.RegisterType<IParser, Parser>();

            container.RegisterType(typeof(IModelBinder<>), typeof(DefaultModelBinder<>));

            container.RegisterType<IValidator, Validator>();

            container.RegisterType<ICommandUser, CommandUser>();
            
            var commandTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetExportedTypes())
                .Where(x => typeof(ICommand).IsAssignableFrom(x) && !x.IsAbstract && x.IsClass && x.IsDefined(typeof(CommandRouteAttribute)));

            foreach (var type in commandTypes)
            {
                var attributeValue = type.GetCustomAttributesData()
                    .First()
                    .ConstructorArguments.Select(z => z.Value)
                    .First().ToString();

                container.RegisterType(typeof(ICommand), type, attributeValue);
            }
            
        }

        public static bool IsRegistered(Type type, string name)
        {
            return container.IsRegistered(type, name);
        }

        public static T Resolve<T>(string name, params ResolverOverride[] overrides)
        {
            return container.Resolve<T>(name, overrides);
        }

        public static T Resolve<T>(string name)
        {
             return container.Resolve<T>(name);
        }

        public static T Resolve<T>(params ResolverOverride[] overrides)
        {
            return container.Resolve<T>(overrides);
        }

        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}
