using System.Collections.Generic;
using System.Linq;
using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Attributes;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Data.Models;
using NikiCars.Models.CarExtraModels;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.CarExtraCommands
{
    [Validate]
    [CommandRoute("list CarExtra")]
    public class ListCarExtraCommand : BaseCommand<ListAllCarExtraModel>
    {
        private IService<Extra> service;
        private IMappingService mapping;

        public ListCarExtraCommand(CommandContext context, IService<Extra> service, IValidator validation, IMappingService mapping)
            : base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(ListAllCarExtraModel item)
        {
            List<Extra> list;
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
                List<ListAllCarExtraModel> carExtraList = list.Select(x => this.mapping.Map<ListAllCarExtraModel>(x))
                    .ToList();

                return this.Success(carExtraList);
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
