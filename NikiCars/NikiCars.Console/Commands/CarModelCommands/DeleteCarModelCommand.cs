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
    [CommandRoute("delete CarModel")]
    public class DeleteCarModelCommand : BaseCommand<EditCarModelModel>
    {
        private IService<CarModel> service;
        private IMappingService mapping;

        public DeleteCarModelCommand(CommandContext context, IService<CarModel> service, IModelBinder<EditCarModelModel> binder, IValidator validation, IMappingService mapping) 
            : base(context, binder, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(EditCarModelModel item)
        {
            if (this.context.ModelState.HasError)
            {
                return this.Error(this.context.ModelState.ToString());
            }

            if (!this.context.CommandUser.IsAuthenticated || !this.context.CommandUser.UserRoles.Contains(RoleConstants.ADMINISTRATOR))
            {
                return this.AuthenticationError("User is not authenticated");
            }

            CarModel carModel = this.mapping.Map<CarModel>(item);

            if (this.service.Delete(carModel))
            {
                return this.Success<CarModel>();
            }

            return this.Error<CarModel>();
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
