using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Console.Constants;
using NikiCars.Data.Models;
using NikiCars.Models.CarDoorModels;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.CarDoorCommands
{
    [CommandRoute("delete CarDoor")]
    public class DeleteCarDoorCommand : BaseCommand<EditCarDoorsModel>
    {
        private IService<NumberOfDoors> service;
        private IMappingService mapping;

        public DeleteCarDoorCommand(CommandContext context, IService<NumberOfDoors> service, IValidator validation, IMappingService mapping) 
            : base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(EditCarDoorsModel item)
        {
            if (!this.context.CommandUser.IsAuthenticated || !this.context.CommandUser.UserRoles.Contains(RoleConstants.ADMINISTRATOR))
            {
                return this.AuthorizationError();
            }

            if (this.context.ModelState.HasError)
            {
                return this.Error(this.context.ModelState.ToString());
            }

            NumberOfDoors model = this.mapping.Map<NumberOfDoors>(item);

            if (this.service.Delete(model))
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
