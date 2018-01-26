using NikiCars.Console.Input;
using NikiCars.Data.Models;
using NikiCars.Services;

namespace NikiCars.Console.Commands
{
    public class CreateCarMake : BaseCommand<CarMake>
    {

        public CreateCarMake(CommandContext context, IService<CarMake> service) 
            : base(context, service)
        {
            
        }

        protected override CarMake BindModel()
        {
            return new BindModelCarMake().ModelBinder(this.context.Properties);
        }

        public override void Execute()
        {
            CarMake carMake = BindModel();
            this.service.Save(carMake);
        }
    }
}
