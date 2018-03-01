using System.Collections.Generic;
using System.Linq;
using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Attributes;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Data.Models;
using NikiCars.Models.CarModelModels;
using NikiCars.Search;
using NikiCars.Search.Interfaces;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.CarModelCommands
{
    [Validate]
    [CommandRoute("get CarModelByCarMake")]
    public class ViewCarModelsByCarMakeCommand : BaseCommand<ViewCarModelsByCarMakeModel>
    {
        private IService<CarModel> service;
        private IMappingService mapping;

        public ViewCarModelsByCarMakeCommand(CommandContext context, IService<CarModel> service, IValidator validation, IMappingService mapping) 
            : base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(ViewCarModelsByCarMakeModel item)
        {
            IEntitySearch<CarModel> entitySearch = new CarMakeIDSearch(item.CarMakeID, SearchTypeEnum.Equals);
            var searchList = new List<IEntitySearch<CarModel>>();
            searchList.Add(entitySearch);
            List<CarModel> list = this.service.GetAll(searchList);

            var result = list.Select(x => this.mapping.Map<ViewCarModelsByCarMakeModel>(x))
                    .ToList();

            return this.Success(result);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
