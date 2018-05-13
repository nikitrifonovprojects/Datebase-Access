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
    [CommandRoute("list LatestCars")]
    public class ShowLatestCarsCommand : BaseCommand<ShowLatestCarModel>
    {
        private ICarService service;
        private IMappingService mapping;

        public ShowLatestCarsCommand(CommandContext context, ICarService service, IValidator validation, IMappingService mapping) 
            : base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(ShowLatestCarModel item)
        {
            List<Car> list;
            var paging = new Pagination();
            paging.PageNumber = 0;
            paging.PageSize = item.Count;
            var date = new DateOrderBy(OrderByEnum.Descending);
            var order = new List<IEntityOrderBy<Car>>();
            var includes = new List<CarIncludes>() { CarIncludes.Colour, CarIncludes.Extras, CarIncludes.FuelType, CarIncludes.GearboxType, CarIncludes.CarMake, CarIncludes.CarModel };
            order.Add(date);

            list = this.service.GetAll(null, order, paging, includes);

            var cars = list.Select(c => this.mapping.Map<AdvancedSearchReturnModel>(c)).ToList();

            return this.Success(cars);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
