using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Console.Constants;
using NikiCars.Data.Models;
using NikiCars.Models.CarModelModels;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.CarModelCommands
{
    [CommandRoute("add CarModel")]
    public class CreateCarModelCommand : BaseCommand<CreateCarModelModel>
    {
        private IService<CarModel> service;
        private IMappingService mapping;

        public CreateCarModelCommand(CommandContext context, IService<CarModel> service, IModelBinder<CreateCarModelModel> binder, IValidator validation, IMappingService mapping) 
            : base(context, binder, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(CreateCarModelModel item)
        {
            if (!this.context.CommandUser.IsAuthenticated && !this.context.CommandUser.UserRoles.Contains(RoleConstants.ADMINISTRATOR))
            {
                return this.AuthorizationError();
            }

            if (this.context.ModelState.HasError)
            {
                return this.Error(this.context.ModelState.ToString());
            }

            CarModel carModel = this.mapping.Map<CarModel>(item);

            CarModel result = this.service.Save(carModel);

            return this.Success(result);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
