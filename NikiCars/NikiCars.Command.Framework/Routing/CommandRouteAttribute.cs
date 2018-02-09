using System;


namespace NikiCars.Command.Framework.Routing
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CommandRouteAttribute : Attribute
    {
        public string RouteName { get; set; }

        public CommandRouteAttribute(string routeName)
        {
            this.RouteName = routeName;
        }
    }
}
