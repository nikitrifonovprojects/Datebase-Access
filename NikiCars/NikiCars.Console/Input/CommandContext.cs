using System.Collections.Generic;
using NikiCars.Console.Validation;
using NikiCars.Data.Models;

namespace NikiCars.Console.Input
{
    public class CommandContext
    {
        public string RawInput { get; set; }

        public string CommandText { get; set; }

        public Dictionary<string, string> Properties { get; set; }

        public EntityValidationResult ModelState { get; set; }
    }
}
