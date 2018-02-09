using System;
using Unity;
using Unity.Resolution;

namespace NikiCars.Dependancy
{
    public class DependencyContainer : IDependencyContainer
    {
        public static IUnityContainer container = new UnityContainer();
       
        public void RegisterType<TFrom, TTo>() where TTo : TFrom
        {
            container.RegisterType<TFrom, TTo>();
        }

        public void RegisterType(Type from, Type to)
        {
            container.RegisterType(from, to);
        }

        public void RegisterType(Type from, Type to, string name)
        {
            container.RegisterType(from, to, name);
        }

        public bool IsRegistered(Type type, string name)
        {
            return container.IsRegistered(type, name);
        }

        public T Resolve<T>(string name, params ResolverOverride[] overrides)
        {
            return container.Resolve<T>(name, overrides);
        }

        public T Resolve<T>(string name)
        {
             return container.Resolve<T>(name);
        }

        public T Resolve<T>(params ResolverOverride[] overrides)
        {
            return container.Resolve<T>(overrides);
        }

        public T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}
