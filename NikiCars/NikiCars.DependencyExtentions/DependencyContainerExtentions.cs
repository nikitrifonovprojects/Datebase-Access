using System;
using System.Linq;
using System.Reflection;
using NikiCars.Authentication;
using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Input;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Data;
using NikiCars.Data.Interfaces;
using NikiCars.Dependancy;
using NikiCars.Logging;
using NikiCars.Services;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;
using NikiCars.Utility;

namespace DependencyExtentions
{
    public static class DependencyContainerExtentions
    {
        public static IDependencyContainer AddDependencies(this IDependencyContainer container)
        {
            container.RegisterType<IMappingService, MappingService>();

            container.RegisterType<IConfig, DeveloperConfig>();
            //container.RegisterType<IConfig, Config>();
            container.RegisterSingleton<ILogger, Logger>();
            container.RegisterType<IExecutionTimerLogger, ExecutionTimerLogger>();

            container.RegisterType<IDependencyContainer, DependencyContainer>();
            container.RegisterType<IHost, Host>();
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

            container.RegisterType<IValidator, Validator>();

            container.RegisterType<ICommandUser, CommandUser>();

            container.RegisterType<IParser, Parser>();

            container.RegisterType<IModelBinder, DefaultModelBinder>();

            var commandTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetExportedTypes())
                .Where(x => typeof(ICommand).IsAssignableFrom(x) && !x.IsAbstract && x.IsClass && x.IsDefined(typeof(CommandRouteAttribute)));

            foreach (var type in commandTypes)
            {
                var attributeValue = type.GetCustomAttributesData()
                    .First(c => c.AttributeType == typeof(CommandRouteAttribute))
                    .ConstructorArguments
                    .Select(z => z.Value)
                    .First()
                    .ToString();

                container.RegisterType(typeof(ICommand), type, attributeValue);
            }

            return container;
        }
    }
}
