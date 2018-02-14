using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Console.Constants;
using NikiCars.Data.Models;
using NikiCars.Models.CarMakeModels;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.CarMakeCommands
{
    [CommandRoute("edit CarMake")]
    public class EditCarMakeCommand : BaseCommand<EditCarMakeModel>
    {
        private IService<CarMake> service;
        private IMappingService mapping;

        public EditCarMakeCommand(CommandContext context, IService<CarMake> service, IModelBinder<EditCarMakeModel> binder, IValidator validation, IMappingService mapping) 
            : base(context, binder, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(EditCarMakeModel item)
        {
            if (!this.context.CommandUser.IsAuthenticated || !this.context.CommandUser.UserRoles.Contains(RoleConstants.ADMINISTRATOR))
            {
                return this.AuthenticationError();
            }

            if (this.context.ModelState.HasError)
            {
                return this.Error(this.context.ModelState.ToString());
            }

            CarMake carMake = this.mapping.Map<CarMake>(item);

            CarMake result = this.service.Save(carMake);

            return this.Success(result);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
