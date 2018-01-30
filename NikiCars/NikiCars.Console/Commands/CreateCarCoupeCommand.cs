using NikiCars.Console.Input;
using NikiCars.Console.Interfaces;
using NikiCars.Console.Routing;
using NikiCars.Console.Validation;
using NikiCars.Data.Models;
using NikiCars.Services;

namespace NikiCars.Console.Commands
{
    [CommandRoute("add CarCoupe")]
    public class CreateCarCoupeCommand : BaseCommand<CarCoupe>
    {
        private IService<CarCoupe> service;

        public CreateCarCoupeCommand(CommandContext context, IService<CarCoupe> service, IModelBinder<CarCoupe> binder, IValidator validation)
            : base(context, binder, validation)
        {
            this.service = service;
        }

        protected override ICommandResult ExecuteAction(CarCoupe item)
        {
            if (this.context.ModelState.HasError)
            {
                return this.Error(this.context.ModelState.ToString());
            }

            CarCoupe result = this.service.Save(item);
            if (result == null)
            {
                return this.Error($"CarCoupe: {item} creation failed");
            }

            return this.Success(result);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
