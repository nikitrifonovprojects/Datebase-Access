using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;

namespace NikiCars.Command.Framework
{
    public class ServerErrorCommand : BaseCommand<string>
    {
        public ServerErrorCommand(CommandContext context, IValidator validation) 
            : base(context, validation)
        {
        }

        protected override ICommandResult ExecuteAction(string item)
        {
            return this.ServerError();
        }
    }
}
