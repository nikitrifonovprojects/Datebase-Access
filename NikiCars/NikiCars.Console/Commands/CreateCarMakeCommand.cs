using NikiCars.Console.Input;
using NikiCars.Console.Interfaces;
using NikiCars.Console.Routing;
using NikiCars.Console.Validation;
using NikiCars.Data.Models;
using NikiCars.Services;

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

            CarMake result = this.service.Save(item);
            if (result == null)
            {
                return this.Error($"CarMake: {item} creation failed");
            }

            return this.Success(result);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
