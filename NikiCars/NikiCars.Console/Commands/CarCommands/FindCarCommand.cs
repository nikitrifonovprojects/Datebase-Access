using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Attributes;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Data.Models;
using NikiCars.Models.CarModels;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.CarCommands
{
    [Validate]
    [CommandRoute("get Car")]
    public class FindCarCommand : BaseCommand<FindCarModel>
    {
        private IService<Car> service;
        private IMappingService mapping;

        public FindCarCommand(CommandContext context, IService<Car> service, IValidator validation, IMappingService mapping) 
            : base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(FindCarModel item)
        {
            Car result = this.service.GetById(item.ID);
            if (result == null)
            {
                return this.Error($"Car with ID: {item.ID} not found");
            }

            return this.Success(result);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
