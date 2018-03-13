using NikiCars.Command.Validation;

namespace NikiCars.Command.Interfaces
{
    public class CommandContext
    {
        public virtual ICommandUser CommandUser { get; set; }

        public virtual string RawInput { get; set; }

        public virtual string CommandText { get; set; }

        public virtual dynamic Properties { get; set; }

        public virtual EntityValidationResult ModelState { get; set; }

        public virtual ICommandResult ResponseResult { get; set; }

        public virtual string ResultString { get; set; }
    }
}
