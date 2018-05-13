using System.Collections.Generic;
using System.Linq;
using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Attributes;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Data.Models;
using NikiCars.Data.Models.Includes;
using NikiCars.Models.CarModels;
using NikiCars.Search;
using NikiCars.Search.Interfaces;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.CarCommands
{
    [Validate]
    [CommandRoute("list ShortList")]
    public class ShortSearchCarCommand : BaseCommand<ShortSearchCarModel>
    {
        private ICarService service;
        private IMappingService mapping;

        public ShortSearchCarCommand(CommandContext context, ICarService service, IValidator validation, IMappingService mapping) 
            : base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(ShortSearchCarModel item)
        {
            var search = CreateSearch(item);
            var includes = new List<CarIncludes>() { CarIncludes.Colour, CarIncludes.Extras, CarIncludes.FuelType, CarIncludes.GearboxType, CarIncludes.CarMake, CarIncludes.CarModel };

            var list = this.service.GetAll(search, null, item.Paging, includes);

            if (list.Count > 0)
            {
                var cars = list.Select(c => this.mapping.Map<AdvancedSearchReturnModel>(c)).ToList();

                return this.Success(cars);
            }
            else
            {
                return this.Success(list);
            }
        }

        private List<IEntitySearch<Car>> CreateSearch(ShortSearchCarModel item)
        {
            var entitySearches = new List<IEntitySearch<Car>>();

            if (item.CarEngine.HasValue)
            {
                entitySearches.Add(new CarEngineSearch(item.CarEngine.Value, SearchEnum.Equals));
            }
            if (item.CarGearbox.HasValue)
            {
                entitySearches.Add(new CarGearboxIDSearch(item.CarGearbox.Value, SearchEnum.Equals));
            }
            if (item.CarMakeID.HasValue)
            {
                entitySearches.Add(new CarMakeIDSearch(item.CarMakeID.Value, SearchEnum.Equals));
            }
            if (item.CarModelID.HasValue)
            {
                entitySearches.Add(new CarModelIDSearch(item.CarModelID.Value, SearchEnum.Equals));
            }
            if (item.CarPrice.HasValue)
            {
                entitySearches.Add(new CarPriceSearch(item.CarPrice.Value, SearchEnum.LessThan));
            }
            if (item.CarYear.HasValue)
            {
                entitySearches.Add(new CarYearSearch(item.CarYear.Value, SearchEnum.GreaterThan));
            }
            
            if (item.IsForParts.HasValue || item.IsUsed.HasValue)
            {
                var condition = new CarContitionModel();
                condition.IsForParts = item.IsForParts;
                condition.IsUsed = item.IsUsed;

                entitySearches.Add(new CarConditionSearch(condition, SearchEnum.Equals));
            }

            return entitySearches;
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
