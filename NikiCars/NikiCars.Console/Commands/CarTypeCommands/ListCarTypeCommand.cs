using System.Collections.Generic;
using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Attributes;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Data.Models;
using NikiCars.Models.CarTypeModels;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.CarTypeCommands
{
    [Validate]
    [CommandRoute("list CarType")]
    public class ListCarTypeCommand : BaseCommand<ListCarTypeModel>
    {
        private IService<CarType> service;
        private IMappingService mapping;

        public ListCarTypeCommand(CommandContext context, IService<CarType> service, IValidator validation, IMappingService mapping) 
            : base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(ListCarTypeModel item)
        {
            List<CarType> list;
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
                List<ListCarTypeModel> carTypeList = new List<ListCarTypeModel>();
                foreach (var carType in list)
                {
                    carTypeList.Add(this.mapping.Map<ListCarTypeModel>(carType));
                }

                return this.Success(carTypeList);
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
