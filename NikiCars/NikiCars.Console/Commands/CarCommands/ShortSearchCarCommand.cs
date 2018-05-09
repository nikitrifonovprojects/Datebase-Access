using System.Collections.Generic;
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
            var list = new List<Car>();
            var search = CreateSearch(item, list);
            var includes = new List<CarIncludes>() { CarIncludes.Colour, CarIncludes.Extras, CarIncludes.FuelType, CarIncludes.GearboxType, CarIncludes.CarMake, CarIncludes.CarModel };

            list = this.service.GetAll(search, null, item.Paging, includes);

            if (list.Count > 0)
            {
                List<AdvancedSearchReturnModel> cars = new List<AdvancedSearchReturnModel>();

                foreach (var car in list)
                {
                    cars.Add(this.mapping.Map<AdvancedSearchReturnModel>(car));
                }

                return this.Success(cars);
            }
            else
            {
                return this.NotFound();
            }
        }

        private List<IEntitySearch<Car>> CreateSearch(ShortSearchCarModel item, List<Car> list)
        {
            var entitySearches = new List<IEntitySearch<Car>>();

            if (item.CarEngine.HasValue)
            {
                entitySearches.Add(new CarEngineSearch(item.CarEngine.Value, SearchTypeEnum.Equals));
            }
            if (item.CarGearbox.HasValue)
            {
                entitySearches.Add(new CarGearboxIDSearch(item.CarGearbox.Value, SearchTypeEnum.Equals));
            }
            if (item.CarMake != null)
            {
                entitySearches.Add(new CarMakeIDSearch(item.CarMake, SearchTypeEnum.Equals));
            }
            if (item.CarModel != null)
            {
                entitySearches.Add(new CarModelIDSearch(item.CarModel, SearchTypeEnum.Equals));
            }
            if (item.CarPrice.HasValue)
            {
                entitySearches.Add(new CarPriceSearch(item.CarPrice.Value, SearchTypeEnum.LessThan));
            }
            if (item.CarYear.HasValue)
            {
                entitySearches.Add(new CarYearSearch(item.CarYear.Value, SearchTypeEnum.GreaterThan));
            }
            
            if (item.IsForParts.HasValue || item.IsUsed.HasValue)
            {
                var condition = new Dictionary<string, bool>();
                if (item.IsForParts.HasValue)
                {
                    condition.Add("IsForParts", item.IsForParts.Value);
                }
                if (item.IsUsed.HasValue)
                {
                    condition.Add("IsUsed", item.IsUsed.Value);
                }

                entitySearches.Add(new CarConditionSearch(condition, SearchTypeEnum.Equals));
            }

            return entitySearches;
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
