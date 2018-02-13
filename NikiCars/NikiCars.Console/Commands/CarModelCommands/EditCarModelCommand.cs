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
    [CommandRoute("edit CarModel")]
    public class EditCarModelCommand : BaseCommand<EditCarModelModel>
    {
        private IService<CarModel> service;
        private IMappingService mapping;

        public EditCarModelCommand(CommandContext context, IService<CarModel> service, IModelBinder<EditCarModelModel> binder, IValidator validation, IMappingService mapping) 
            : base(context, binder, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(EditCarModelModel item)
        {
            if (!this.context.CommandUser.IsAuthenticated)
            {
                return this.AuthenticationError("User not logged in");
            }

            CarModel carModel = this.mapping.Map<CarModel>(item);

            if (!this.context.CommandUser.UserRoles.Contains(RoleConstants.ADMINISTRATOR))
            {
                return this.AuthorizationError("User does not have permission");
            }

            CarModel result = this.service.Save(carModel);

            return this.Success(result);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
