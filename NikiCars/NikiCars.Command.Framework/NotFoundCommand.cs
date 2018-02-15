using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;

namespace NikiCars.Command.Framework
{
    public class NotFoundCommand : BaseCommand<string>
    {
        public NotFoundCommand(CommandContext context, IValidator validation) 
            : base(context, validation)
        {
        }

        protected override ICommandResult ExecuteAction(string item)
        {
            return this.NotFound();
        }
    }
}
