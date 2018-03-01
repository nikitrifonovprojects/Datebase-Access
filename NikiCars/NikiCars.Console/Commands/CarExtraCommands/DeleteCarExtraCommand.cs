using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Attributes;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Console.Constants;
using NikiCars.Data.Models;
using NikiCars.Models.CarExtraModels;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.CarExtraCommands
{
    [Validate]
    [Authorization(RoleConstants.ADMINISTRATOR)]
    [CommandRoute("delete CarExtra")]
    public class DeleteCarExtraCommand : BaseCommand<EditCarExtraModel>
    {
        private IService<Extra> service;
        private IMappingService mapping;

        public DeleteCarExtraCommand(CommandContext context, IService<Extra> service, IValidator validation, IMappingService mapping)
            : base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(EditCarExtraModel item)
        {
            Extra model = this.mapping.Map<Extra>(item);

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
