using System.Collections.Generic;
using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Data.Models;
using NikiCars.Models.CarModelModels;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.CarModelCommands
{
    [CommandRoute("list CarModel")]
    public class ListCarModelCommand : BaseCommand<ListCarModelModel>
    {
        private IService<CarModel> service;
        private IMappingService mapping;

        public ListCarModelCommand(CommandContext context, IService<CarModel> service,  IModelBinder<ListCarModelModel> binder, IValidator validation, IMappingService mapping) 
            : base(context, binder, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(ListCarModelModel item)
        {
            List<CarModel> list;
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
                List<ListCarModelModel> carModellist = new List<ListCarModelModel>();
                foreach (var carModel in list)
                {
                    carModellist.Add(this.mapping.Map<ListCarModelModel>(carModel));
                }

                return this.Success(carModellist);
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
