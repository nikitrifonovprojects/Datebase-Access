using NikiCars.Console.Input;
using NikiCars.Console.Interfaces;
using NikiCars.Console.Validation;
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
            var IsValid = ValidationHelper.ValidateEntity(item);

            if (IsValid.HasError)
            {
                return this.Error(IsValid.ToString());
            }

            CarCoupe result = this.service.Save(item);
            if (result == null)
            {
                return this.Error($"CarCoupe: {item} creation failed");
            }

            return this.Success(result);
        }
    }
}
