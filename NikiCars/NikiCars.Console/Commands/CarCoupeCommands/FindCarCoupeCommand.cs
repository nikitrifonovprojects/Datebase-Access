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
    [CommandRoute("get CarCoupe")]
    public class FindCarCoupeCommand : BaseCommand<EditCarCoupeModel>
    {
        private IService<CarCoupe> service;
        private IMappingService mapping;

        public FindCarCoupeCommand(CommandContext context, IService<CarCoupe> service, IValidator validation, IMappingService mapping) 
            :base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(EditCarCoupeModel item)
        {
            CarCoupe carCoupe = this.mapping.Map<CarCoupe>(item);

            CarCoupe result = this.service.GetById(carCoupe.ID);
            if (result == null)
            {
                return this.Error($"CarCoupe with ID: {carCoupe.ID} not found");
            }
            
            return this.Success(result);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
