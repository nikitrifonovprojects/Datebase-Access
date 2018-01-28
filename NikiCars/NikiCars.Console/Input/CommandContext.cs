using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NikiCars.Console.Input
{
    public class CommandContext
    {
        [Required]
        public string RawInput { get; set; }

        [Required]
        public string CommandText { get; set; }

        [Required]
        public Dictionary<string, string> Properties { get; set; }

    }
}
