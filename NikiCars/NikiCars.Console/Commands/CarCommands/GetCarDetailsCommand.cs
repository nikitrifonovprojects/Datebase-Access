using System.Collections.Generic;
using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Attributes;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Data.Models;
using NikiCars.Data.Models.Includes;
using NikiCars.Models.CarModels;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.CarCommands
{
    [Validate]
    [CommandRoute("get CarDetails")]
    public class GetCarDetailsCommand : BaseCommand<ShowCarDetailsModel>
    {
        private ICarService service;
        private IMappingService mapping;

        public GetCarDetailsCommand(CommandContext context, ICarService service, IValidator validation, IMappingService mapping) 
            : base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(ShowCarDetailsModel item)
        {
            var includes = new List<CarIncludes>() { CarIncludes.Colour, CarIncludes.Extras, CarIncludes.FuelType, CarIncludes.GearboxType, CarIncludes.CarMake, CarIncludes.CarModel };
            Car car = this.service.GetById(item.ID, includes);

            if (car == null)
            {
                return this.Error($"Car with ID: {item.ID} not found");
            }

            var result = this.mapping.Map<AdvancedSearchCarModel>(car);

            return this.Success(result);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
