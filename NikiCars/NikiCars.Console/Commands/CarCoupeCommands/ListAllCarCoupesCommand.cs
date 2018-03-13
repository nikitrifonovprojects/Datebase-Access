using System.Collections.Generic;
using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Attributes;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Data.Models;
using NikiCars.Models.CarCoupeModels;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.CarCoupeCommands
{
    [Validate]
    [CommandRoute("list CarCoupe")]
    public class ListAllCarCoupesCommand : BaseCommand<string>
    {
        private IService<CarCoupe> service;
        private IMappingService mapping;

        public ListAllCarCoupesCommand(CommandContext context, IService<CarCoupe> service, IValidator validation, IMappingService mapping)
            : base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(string item)
        {
            List<CarCoupe> list;
            list = this.service.GetAll();

            if (list.Count > 0)
            {
                return this.Success(list);
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
