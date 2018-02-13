using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Console.Constants;
using NikiCars.Data.Models;
using NikiCars.Models.CarTypeModels;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.CarTypeCommands
{
    [CommandRoute("edit CarType")]
    public class EditCarTypeCommand : BaseCommand<EditCarTypeModel>
    {
        private IService<CarType> service;
        private IMappingService mapping;

        public EditCarTypeCommand(CommandContext context, IService<CarType> service, IModelBinder<EditCarTypeModel> binder, IValidator validation, IMappingService mapping) 
            : base(context, binder, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(EditCarTypeModel item)
        {
            if (!this.context.CommandUser.IsAuthenticated)
            {
                return this.AuthenticationError("User not logged in");
            }

            CarType carType = this.mapping.Map<CarType>(item);

            if (!this.context.CommandUser.UserRoles.Contains(RoleConstants.ADMINISTRATOR))
            {
                return this.AuthorizationError("User does not have permission");
            }

            CarType result = this.service.Save(carType);

            return this.Success(result);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
