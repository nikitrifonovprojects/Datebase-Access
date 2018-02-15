using NikiCars.Command.Validation;

namespace NikiCars.Command.Interfaces
{
    public class CommandContext
    {
        public ICommandUser CommandUser { get; set; }

        public string RawInput { get; set; }

        public string CommandText { get; set; }

        public dynamic Properties { get; set; }

        public EntityValidationResult ModelState { get; set; }

        public ICommandResult ResponseResult { get; set; }
    }
}
