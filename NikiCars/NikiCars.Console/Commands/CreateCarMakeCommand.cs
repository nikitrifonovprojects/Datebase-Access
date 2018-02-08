using NikiCars.Console.Input;
using NikiCars.Command.Interfaces;
using NikiCars.Console.Routing;
using NikiCars.Command.Validation;
using NikiCars.Data.Models;
using NikiCars.Services;
using NikiCars.Services.Interfaces;

namespace NikiCars.Console.Commands
{
    [CommandRoute("add CarMake")]
    public class CreateCarMakeCommand : BaseCommand<CarMake>
    {
        private IService<CarMake> service;

        public CreateCarMakeCommand(CommandContext context, IService<CarMake> service, IModelBinder<CarMake> binder, IValidator validation) 
            : base(context, binder, validation)
        {
            this.service = service;
        }

        protected override ICommandResult ExecuteAction(CarMake item)
        {
            if (this.context.ModelState.HasError)
            {
                return this.Error(this.context.ModelState.ToString());
            }

            if (!this.context.CommandUser.IsAuthenticated)
            {
                return this.AuthenticationError("User is not authenticated");
            }

            CarMake result = this.service.Save(item);

            return this.Success(result);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
