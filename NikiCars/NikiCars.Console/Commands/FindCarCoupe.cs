using NikiCars.Console.Input;
using NikiCars.Console.Interfaces;
using NikiCars.Data.Models;
using NikiCars.Services;

namespace NikiCars.Console.Commands
{
    public class FindCarCoupe : BaseCommand<CarCoupe>
    {
        public FindCarCoupe(CommandContext context, IService<CarCoupe> service, IModelBinder<CarCoupe> binder) 
            :base(context, service, binder)
        {
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
    }
}
