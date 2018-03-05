using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Attributes;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Console.Constants;
using NikiCars.Data.Models;
using NikiCars.Models.CarCoupeModels;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.CarCoupeCommands
{
    [Validate]
    [Authorization(RoleConstants.ADMINISTRATOR)]
    [CommandRoute("edit CarCoupe")]
    public class EditCarCoupeCommand : BaseCommand<EditCarCoupeModel> 
    {
        private IService<CarCoupe> service;
        private IMappingService mapping;

        public EditCarCoupeCommand(CommandContext context, IService<CarCoupe> service, IValidator validation, IMappingService mapping) 
            : base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(EditCarCoupeModel item)
        {
            CarCoupe dbCarCoupe = this.service.GetById(item.ID);

            if (dbCarCoupe == null)
            {
                return this.Error(item.ID);
            }

            dbCarCoupe = this.mapping.Map(item, dbCarCoupe);

            CarCoupe result = this.service.Save(dbCarCoupe);

            return this.Success(result);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
