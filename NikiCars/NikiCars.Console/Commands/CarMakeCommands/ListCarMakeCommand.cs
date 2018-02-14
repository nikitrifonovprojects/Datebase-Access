using System.Collections.Generic;
using System.Linq;
using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Data.Models;
using NikiCars.Models.CarMakeModels;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.CarMakeCommands
{
    [CommandRoute("list CarMake")]
    public class ListCarMakeCommand : BaseCommand<ListCarMakeModel>
    {
        private IService<CarMake> service;
        private IMappingService mapping;

        public ListCarMakeCommand(CommandContext context, IService<CarMake> service, IModelBinder<ListCarMakeModel> binder, IValidator validation, IMappingService mapping) 
            : base(context, binder, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(ListCarMakeModel item)
        {
            List<CarMake> list;
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
                List<ListCarMakeModel> carMakellist = new List<ListCarMakeModel>();
                foreach (var carMake in list)
                {
                    carMakellist.Add(this.mapping.Map<ListCarMakeModel>(carMake));
                }

                List<ListCarMakeModel> ordered = carMakellist.OrderBy(x => x.Name).ToList();

                return this.Success(ordered);
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
