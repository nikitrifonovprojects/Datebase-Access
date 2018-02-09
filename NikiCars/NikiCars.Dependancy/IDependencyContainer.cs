using System;
using Unity.Resolution;

namespace NikiCars.Dependancy
{
    public interface IDependencyContainer
    {
        bool IsRegistered(Type type, string name);

        void RegisterType<TFrom, TTo>() where TTo : TFrom;

        void RegisterType(Type from, Type to);

        void RegisterType(Type from, Type to, string name);

        T Resolve<T>();

        T Resolve<T>(params DependencyObject[] overrides);

        T Resolve<T>(string name);

        T Resolve<T>(string name, params DependencyObject[] overrides);
    }
}