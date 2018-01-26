using System.Collections.Generic;

namespace NikiCars.Console.Input
{
    public class CommandContext
    {
        public string RawInput { get; set; }

        public string CommandText { get; set; }

        public Dictionary<string, string> Properties { get; set; }

    }
}
