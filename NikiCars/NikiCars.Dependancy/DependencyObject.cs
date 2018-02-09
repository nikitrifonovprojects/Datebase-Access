using System;

namespace NikiCars.Dependancy
{
    public class DependencyObject
    {
        public Type TypeToConstruct { get; }

        public object DependencyValue { get; }

        public DependencyObject(Type typeToConstruct, object dependencyValue)
        {
            this.TypeToConstruct = typeToConstruct;
            this.DependencyValue = dependencyValue;
        }
    }
}
