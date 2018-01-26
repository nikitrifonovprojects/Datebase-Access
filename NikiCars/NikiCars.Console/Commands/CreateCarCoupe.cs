using NikiCars.Console.Input;
using NikiCars.Console.Interfaces;
using NikiCars.Data.Models;
using NikiCars.Services;

namespace NikiCars.Console.Commands
{
    public class CreateCarCoupe : BaseCommand<CarCoupe>
    {
        public CreateCarCoupe(CommandContext context, IService<CarCoupe> service, IModelBinder<CarCoupe> binder)
            : base(context, service, binder)
        {
        }

        protected override ICommandResult ExecuteAction(CarCoupe item)
        {
            CarCoupe result = this.service.Save(item);
            if (result == null)
            {
                return this.Error($"CarCoupe: {item} creation failed", item);
            }

            return this.Success($"CarCoupe: {result} creation success", result);
        }
    }
}
