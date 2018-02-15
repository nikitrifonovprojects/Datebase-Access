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
    [CommandRoute("delete CarType")]
    public class DeleteCarTypeCommand : BaseCommand<EditCarTypeModel>
    {
        private IService<CarType> service;
        private IMappingService mapping;

        public DeleteCarTypeCommand(CommandContext context, IService<CarType> service, IValidator validation, IMappingService mapping) 
            : base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(EditCarTypeModel item)
        {
            if (!this.context.CommandUser.IsAuthenticated || !this.context.CommandUser.UserRoles.Contains(RoleConstants.ADMINISTRATOR))
            {
                return this.AuthenticationError();
            }

            if (this.context.ModelState.HasError)
            {
                return this.Error(this.context.ModelState.ToString());
            }

            CarType carType = this.mapping.Map<CarType>(item);

            if (this.service.Delete(carType))
            {
                return this.Success();
            }

            return this.Error();
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
