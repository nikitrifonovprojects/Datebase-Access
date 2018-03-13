using System.Collections.Generic;
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
    public class ListCarExtraCommand : BaseCommand<string>
    {
        private IService<Extra> service;
        private IMappingService mapping;

        public ListCarExtraCommand(CommandContext context, IService<Extra> service, IValidator validation, IMappingService mapping)
            : base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(string item)
        {
            var list = this.service.GetAll();

            return this.Success(list);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
