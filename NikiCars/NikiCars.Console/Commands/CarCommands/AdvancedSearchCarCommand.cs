using System;
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
            var list = new List<Car>();
            var search = CreateSearch(item);
            var order = CreateOrderBy(item);
            var includes = new List<CarIncludes>() { CarIncludes.Colour, CarIncludes.Extras, CarIncludes.FuelType, CarIncludes.GearboxType, CarIncludes.CarMake, CarIncludes.CarModel };

            list = this.service.GetAll(search, order, item.Paging, includes);

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
            if (item.FromCarPrice.HasValue)
            {
                entitySearches.Add(new CarPriceSearch(item.FromCarPrice.Value, SearchTypeEnum.GreaterThan));
            }
            if (item.ToCarPrice.HasValue)
            {
                entitySearches.Add(new CarPriceSearch(item.ToCarPrice.Value, SearchTypeEnum.LessThan));
            }
            if (item.FromCarYear.HasValue)
            {
                entitySearches.Add(new CarYearSearch(item.FromCarYear.Value, SearchTypeEnum.GreaterThan));
            }
            if (item.ToCarYear.HasValue)
            {
                entitySearches.Add(new CarYearSearch(item.ToCarYear.Value, SearchTypeEnum.LessThan));
            }
            if (item.FromHorsePower.HasValue)
            {
                entitySearches.Add(new CarHorsePowerSearch(item.FromHorsePower.Value, SearchTypeEnum.GreaterThan));
            }
            if (item.ToHorsePower.HasValue)
            {
                entitySearches.Add(new CarHorsePowerSearch(item.ToHorsePower.Value, SearchTypeEnum.LessThan));
            }
            if (item.Kilometers.HasValue)
            {
                entitySearches.Add(new CarKilometersSearch(item.Kilometers.Value, SearchTypeEnum.LessThan));
            }
            if (item.ColourID.HasValue)
            {
                entitySearches.Add(new CarColourSearch(item.ColourID.Value, SearchTypeEnum.Equals));
            }
            if (item.IsForParts.HasValue || item.IsUsed.HasValue || item.IsLeftSteering.HasValue)
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
                if (item.IsLeftSteering.HasValue)
                {
                    condition.Add("IsLeftSteering", item.IsLeftSteering.Value);
                }

                entitySearches.Add(new CarConditionSearch(condition, SearchTypeEnum.Equals));
            }
            if (item.ExcludedExtras != null)
            {
                entitySearches.Add(new CarExtrasSearch(item.ExcludedExtras, SearchTypeEnum.NotEquals));
            }
            if (item.IncludedExtras != null)
            {
                entitySearches.Add(new CarExtrasSearch(item.IncludedExtras, SearchTypeEnum.Equals));
            }
            if (item.OrderBy == CarOrderByEnum.Last2Days)
            {
                var latest = new CarDateCreatedSearch(DateTime.Now.AddDays(-2), SearchTypeEnum.GreaterOrEquals);
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
