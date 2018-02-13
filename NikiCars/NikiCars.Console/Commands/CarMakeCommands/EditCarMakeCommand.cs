using NikiCars.Command.Framework;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Console.Constants;
using NikiCars.Data.Models;
using NikiCars.Models.CarMakeModels;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.CarMakeCommands
{
    public class EditCarMakeCommand : BaseCommand<CreateCarMakeModel>
    {
        private IService<CarMake> service;
        private IMappingService mapping;

        public EditCarMakeCommand(CommandContext context, IService<CarMake> service, IModelBinder<CreateCarMakeModel> binder, IValidator validation, IMappingService mapping) 
            : base(context, binder, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(CreateCarMakeModel item)
        {
            if (!this.context.CommandUser.IsAuthenticated)
            {
                return this.AuthenticationError("User not logged in");
            }

            CarMake carMake = this.mapping.Map<CarMake>(item);

            if (!this.context.CommandUser.UserRoles.Contains(RoleConstants.ADMINISTRATOR))
            {
                return this.AuthorizationError("User does not have permission");
            }

            CarMake result = this.service.Save(carMake);

            return this.Success(result);
        }
    }
}
