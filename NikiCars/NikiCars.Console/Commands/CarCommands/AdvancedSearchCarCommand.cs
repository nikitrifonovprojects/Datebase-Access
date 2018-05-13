using System;
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
    [CommandRoute("list AdvancedList")]
    public class AdvancedSearchCarCommand : BaseCommand<AdvancedSearchCarModel>
    {
        private ICarService service;
        private IMappingService mapping;

        public AdvancedSearchCarCommand(CommandContext context, ICarService service, IValidator validation, IMappingService mapping)
            : base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(AdvancedSearchCarModel item)
        {
            var search = CreateSearch(item);
            var order = CreateOrderBy(item);
            var includes = new List<CarIncludes>() { CarIncludes.Colour, CarIncludes.Extras, CarIncludes.FuelType, CarIncludes.GearboxType, CarIncludes.CarMake, CarIncludes.CarModel };

            var list = this.service.GetAll(search, order, item.Paging, includes);

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

        private List<IEntityOrderBy<Car>> CreateOrderBy(AdvancedSearchCarModel item)
        {
            var order = new List<IEntityOrderBy<Car>>();

            if (item.OrderBy == CarOrderByEnum.FirstRegistration)
            {
                var registration = new CarRegistrationOrderBy();
                order.Add(registration);
            }
            if (item.OrderBy == CarOrderByEnum.Kilometers)
            {
                var kilometers = new CarKilometersOrderBy();
                order.Add(kilometers);
            }
            if (item.OrderBy == CarOrderByEnum.Latest)
            {
                var latest = new DateOrderBy();
                order.Add(latest);
            }
            if (item.OrderBy == CarOrderByEnum.Price)
            {
                var latest = new CarPriceOrderBy();
                order.Add(latest);
            }
            if (item.OrderBy == CarOrderByEnum.MakeModelPrice)
            {
                var latest = new CarMakeModelPriceOrderBy();
                order.Add(latest);
            }
            if (item.OrderBy == CarOrderByEnum.Last2Days)
            {
                var latest = new DateOrderBy();
                order.Add(latest);
            }

            return order;
        }

        private List<IEntitySearch<Car>> CreateSearch(AdvancedSearchCarModel item)
        {
            var entitySearches = new List<IEntitySearch<Car>>();

            if (item.CarGearbox.HasValue)
            {
                entitySearches.Add(new CarGearboxIDSearch(item.CarGearbox.Value, SearchEnum.Equals));
            }
            if (item.CarMakeIds != null)
            {
                entitySearches.Add(new CarMakeIDSearch(item.CarMakeIds, SearchEnum.Equals));
            }
            if (item.CarModelIds != null)
            {
                entitySearches.Add(new CarModelIDSearch(item.CarModelIds, SearchEnum.Equals));
            }
            if (item.FromCarPrice.HasValue)
            {
                entitySearches.Add(new CarPriceSearch(item.FromCarPrice.Value, SearchEnum.GreaterThan));
            }
            if (item.ToCarPrice.HasValue)
            {
                entitySearches.Add(new CarPriceSearch(item.ToCarPrice.Value, SearchEnum.LessThan));
            }
            if (item.FromCarYear.HasValue)
            {
                entitySearches.Add(new CarYearSearch(item.FromCarYear.Value, SearchEnum.GreaterThan));
            }
            if (item.ToCarYear.HasValue)
            {
                entitySearches.Add(new CarYearSearch(item.ToCarYear.Value, SearchEnum.LessThan));
            }
            if (item.FromHorsePower.HasValue)
            {
                entitySearches.Add(new CarHorsePowerSearch(item.FromHorsePower.Value, SearchEnum.GreaterThan));
            }
            if (item.ToHorsePower.HasValue)
            {
                entitySearches.Add(new CarHorsePowerSearch(item.ToHorsePower.Value, SearchEnum.LessThan));
            }
            if (item.Kilometers.HasValue)
            {
                entitySearches.Add(new CarKilometersSearch(item.Kilometers.Value, SearchEnum.LessThan));
            }
            if (item.ColourID.HasValue)
            {
                entitySearches.Add(new CarColourSearch(item.ColourID.Value, SearchEnum.Equals));
            }
            if (item.IsForParts.HasValue || item.IsUsed.HasValue)
            {
                var condition = new CarContitionModel();
                condition.IsForParts = item.IsForParts;
                condition.IsUsed = item.IsUsed;
               

                entitySearches.Add(new CarConditionSearch(condition, SearchEnum.Equals));
            }
            if (item.ExcludedExtrasIds != null)
            {
                entitySearches.Add(new CarExtrasSearch(item.ExcludedExtrasIds, SearchEnum.NotEquals));
            }
            if (item.IncludedExtrasIds != null)
            {
                entitySearches.Add(new CarExtrasSearch(item.IncludedExtrasIds, SearchEnum.Equals));
            }
            if (item.OrderBy == CarOrderByEnum.Last2Days)
            {
                var latest = new CarDateCreatedSearch(DateTime.Now.AddDays(-2), SearchEnum.GreaterOrEquals);
                entitySearches.Add(latest);
            }
            return entitySearches;
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
