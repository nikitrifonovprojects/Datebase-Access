using NikiCars.Console.Input;
using NikiCars.Console.Interfaces;
using NikiCars.Data.Models;
using NikiCars.Services;

namespace NikiCars.Console.Commands
{
    public class CreateCarMake : BaseCommand<CarMake>
    {
        public CreateCarMake(CommandContext context, IService<CarMake> service, IModelBinder<CarMake> binder) 
            : base(context, service, binder)
        {
        }

        protected override ICommandResult ExecuteAction(CarMake item)
        {
            CarMake result = this.service.Save(item);
            if (result == null)
            {
                return this.Error($"CarMake: {item} creation failed", item);
            }

            return this.Success($"CarMake: {result} creation success", result);
        }
    }
}
