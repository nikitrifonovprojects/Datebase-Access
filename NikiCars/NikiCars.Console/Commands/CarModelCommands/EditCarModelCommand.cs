using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Attributes;
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
    [Validate]
    [Authorization(RoleConstants.ADMINISTRATOR)]
    [CommandRoute("edit CarModel")]
    public class EditCarModelCommand : BaseCommand<EditCarModelModel>
    {
        private IService<CarModel> service;
        private IMappingService mapping;

        public EditCarModelCommand(CommandContext context, IService<CarModel> service, IValidator validation, IMappingService mapping) 
            : base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(EditCarModelModel item)
        {
            CarModel dbCarModel = this.service.GetById(item.ID);

            if (dbCarModel == null)
            {
                return this.Error(item.ID);
            }

            dbCarModel = this.mapping.Map(item, dbCarModel);
            CarModel result = this.service.Save(dbCarModel);

            return this.Success(result);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
