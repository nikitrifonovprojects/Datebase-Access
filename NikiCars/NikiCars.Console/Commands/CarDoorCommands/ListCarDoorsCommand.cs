using System.Collections.Generic;
using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Data.Models;
using NikiCars.Models.CarDoorModels;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.CarDoorCommands
{
    [CommandRoute("list CarDoor")]
    public class ListCarDoorsCommand : BaseCommand<ListAllCarDoorsModel>
    {
        private IService<NumberOfDoors> service;
        private IMappingService mapping;

        public ListCarDoorsCommand(CommandContext context, IService<NumberOfDoors> service, IModelBinder<ListAllCarDoorsModel> binder, IValidator validation, IMappingService mapping) 
            : base(context, binder, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(ListAllCarDoorsModel item)
        {
            List<NumberOfDoors> list;
            if (item.PageNumber != 0 || item.PageSize != 0)
            {
                list = this.service.GetAll(item.PageNumber, item.PageSize);
            }
            else
            {
                list = this.service.GetAll();
            }

            if (list.Count > 0)
            {
                List<ListAllCarDoorsModel> carCoupelist = new List<ListAllCarDoorsModel>();
                foreach (var carCoupe in list)
                {
                    carCoupelist.Add(this.mapping.Map<ListAllCarDoorsModel>(carCoupe));
                }

                return this.Success(carCoupelist);
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
