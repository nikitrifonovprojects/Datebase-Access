using System.Collections.Generic;
using NikiCars.Console.Interfaces;
using NikiCars.Console.Validation;

namespace NikiCars.Console.Input
{
    public class CommandContext
    {
        public ICommandUser CommandUser { get; set; }

        public string RawInput { get; set; }

        public string CommandText { get; set; }

        public Dictionary<string, string> Properties { get; set; }

        public EntityValidationResult ModelState { get; set; }
    }
}
