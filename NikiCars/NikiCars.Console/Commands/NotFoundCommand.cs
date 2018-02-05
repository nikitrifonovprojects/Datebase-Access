using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;

namespace NikiCars.Console.Commands
{
    public class NotFoundCommand : BaseCommand<string>
    {
        public NotFoundCommand(CommandContext context, IModelBinder<string> binder, IValidator validation) 
            : base(context, binder, validation)
        {
        }

        protected override ICommandResult ExecuteAction(string item)
        {
            return this.NotFound<string>();
        }
    }
}
