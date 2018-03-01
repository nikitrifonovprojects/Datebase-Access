using System.Collections.Generic;
using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Attributes;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Data.Models;
using NikiCars.Models.CarMakeModels;
using NikiCars.Search;
using NikiCars.Search.Interfaces;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.CarMakeCommands
{
    [Validate]
    [CommandRoute("list CarMake")]
    public class ListCarMakeCommand : BaseCommand<ListCarMakeModel>
    {
        private IService<CarMake> service;
        private IMappingService mapping;

        public ListCarMakeCommand(CommandContext context, IService<CarMake> service, IValidator validation, IMappingService mapping) 
            : base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(ListCarMakeModel item)
        {
            List<CarMake> list;
            var name = new NameOrderBy(OrderByEnum.Descending);
            var order = new List<IEntityOrderBy<CarMake>>();
            order.Add(name);

            list = this.service.GetAll(order, item.Paging);

            if (list.Count > 0)
            {
                List<ListCarMakeModel> carMakellist = new List<ListCarMakeModel>();
                foreach (var carMake in list)
                {
                    carMakellist.Add(this.mapping.Map<ListCarMakeModel>(carMake));
                }

                return this.Success(carMakellist);
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
