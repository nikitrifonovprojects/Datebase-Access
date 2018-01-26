using NikiCars.Console.Input;
using NikiCars.Data.Models;
using NikiCars.Services;

namespace NikiCars.Console.Commands
{
    public class CreateCarCoupe : BaseCommand<CarCoupe>
    {
        public CreateCarCoupe(CommandContext context, IService<CarCoupe> service)
            : base(context, service)
        {

        }

        protected override CarCoupe BindModel()
        {
            return new BindModelCarCoupe().ModelBinder(this.context.Properties); 
        }

        public override void Execute()
        {
            CarCoupe carCoupe = BindModel();
            this.service.Save(carCoupe);
        }
    }
}
