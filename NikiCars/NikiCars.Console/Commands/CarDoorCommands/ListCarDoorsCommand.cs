using System.Collections.Generic;
using System.Linq;
using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Attributes;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Data.Models;
using NikiCars.Models.CarDoorModels;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.CarDoorCommands
{
    [Validate]
    [CommandRoute("list CarDoor")]
    public class ListCarDoorsCommand : BaseCommand<ListAllCarDoorsModel>
    {
        private IService<NumberOfDoors> service;
        private IMappingService mapping;

        public ListCarDoorsCommand(CommandContext context, IService<NumberOfDoors> service, IValidator validation, IMappingService mapping) 
            : base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(ListAllCarDoorsModel item)
        {
            List<NumberOfDoors> list;
            if (item.Paging != null)
            {
                list = this.service.GetAll(item.Paging);

            }
            else
            {
                list = this.service.GetAll();
            }

            if (list.Count > 0)
            {
                List<ListAllCarDoorsModel> carDoorsList = list.Select(x => this.mapping.Map<ListAllCarDoorsModel>(x))
                    .ToList();

                return this.Success(carDoorsList);
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
