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
    [CommandRoute("edit CarExtra")]
    public class EditCarExtraCommand : BaseCommand<EditCarExtraModel>
    {
        private IService<Extra> service;
        private IMappingService mapping;

        public EditCarExtraCommand(CommandContext context, IService<Extra> service, IValidator validation, IMappingService mapping) 
            : base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(EditCarExtraModel item)
        {
            Extra dbExtra = this.service.GetById(item.ID);

            if (dbExtra == null)
            {
                return this.Error(item.ID);
            }

            dbExtra = this.mapping.Map(item, dbExtra);
            Extra result = this.service.Save(dbExtra);

            return this.Success(result);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
