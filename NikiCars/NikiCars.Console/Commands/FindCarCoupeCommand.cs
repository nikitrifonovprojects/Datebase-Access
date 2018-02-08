using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Console.Routing;
using NikiCars.Data.Models;
using NikiCars.Services.Interfaces;

namespace NikiCars.Console.Commands
{
    [CommandRoute("get CarCoupe")]
    public class FindCarCoupeCommand : BaseCommand<CarCoupe>
    {
        private IService<CarCoupe> service;

        public FindCarCoupeCommand(CommandContext context, IService<CarCoupe> service, IModelBinder<CarCoupe> binder, IValidator validation) 
            :base(context, binder, validation)
        {
            this.service = service;
        }

        protected override ICommandResult ExecuteAction(CarCoupe item)
        {
            CarCoupe result = this.service.GetById(item.ID);
            if (result == null)
            {
                return this.Error($"CarCoupe with ID: {item.ID} not found");
            }
            
            return this.Success(result);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
