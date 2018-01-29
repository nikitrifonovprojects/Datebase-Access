using NikiCars.Console.Input;
using NikiCars.Console.Interfaces;
using NikiCars.Console.Validation;

namespace NikiCars.Console.Commands
{
    public class ServerErrorCommand : BaseCommand<string>
    {
        public ServerErrorCommand(CommandContext context, IModelBinder<string> binder, IValidator validation) 
            : base(context, binder, validation)
        {
        }

        protected override ICommandResult ExecuteAction(string item)
        {
            return this.ServerError<string>();
        }
    }
}
