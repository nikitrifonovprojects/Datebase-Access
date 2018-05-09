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
    [CommandRoute("list LatestCars")]
    public class ShowLatestCarsCommand : BaseCommand<ListShortCarModel>
    {
        private ICarService service;
        private IMappingService mapping;

        public ShowLatestCarsCommand(CommandContext context, ICarService service, IValidator validation, IMappingService mapping) 
            : base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(ListShortCarModel item)
        {
            List<Car> list;
            var date = new DateOrderBy(OrderByEnum.Descending);
            var search = new List<IEntitySearch<Car>>() { new CarDateCreatedSearch(DateTime.Now.AddDays(-2), SearchTypeEnum.GreaterOrEquals) };
            var order = new List<IEntityOrderBy<Car>>();
            var includes = new List<CarIncludes>() { CarIncludes.Colour, CarIncludes.Extras, CarIncludes.FuelType, CarIncludes.GearboxType, CarIncludes.CarMake, CarIncludes.CarModel };
            order.Add(date);

            list = this.service.GetAll(search, order, item.Paging, includes);

            if (list.Count > 0)
            {
                List<AdvancedSearchCarModel> cars = new List<AdvancedSearchCarModel>();

                foreach (var car in list)
                {
                    cars.Add(this.mapping.Map<AdvancedSearchCarModel>(car));
                }

                return this.Success(cars);
            }
            else
            {
                return this.NotFound();
            }
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
