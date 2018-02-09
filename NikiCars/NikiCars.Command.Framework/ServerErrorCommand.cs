using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;

namespace NikiCars.Command.Framework
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
