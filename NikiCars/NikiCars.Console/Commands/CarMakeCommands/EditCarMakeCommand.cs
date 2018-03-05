using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Attributes;
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
    [Validate]
    [Authorization(RoleConstants.ADMINISTRATOR)]
    [CommandRoute("edit CarMake")]
    public class EditCarMakeCommand : BaseCommand<EditCarMakeModel>
    {
        private IService<CarMake> service;
        private IMappingService mapping;

        public EditCarMakeCommand(CommandContext context, IService<CarMake> service, IValidator validation, IMappingService mapping) 
            : base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(EditCarMakeModel item)
        {
            CarMake dbCarMake = this.service.GetById(item.ID);

            if (dbCarMake == null)
            {
                return this.Error(item.ID);
            }

            dbCarMake = this.mapping.Map(item, dbCarMake);
            CarMake result = this.service.Save(dbCarMake);

            return this.Success(result);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
