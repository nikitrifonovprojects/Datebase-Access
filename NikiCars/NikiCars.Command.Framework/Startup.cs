using System.Collections.Generic;
using NikiCars.Command.Framework.Attributes;

namespace NikiCars.Command.Framework
{
    public static class Startup
    {
        internal static List<BaseCommandAttribute> Filters { get; set; }

        static Startup()
        {
            Filters = new List<BaseCommandAttribute>();
        }

        public static void AddGlobalFilter(BaseCommandAttribute commandAttribute) 
        {
            Filters.Add(commandAttribute);
        }
    }
}
