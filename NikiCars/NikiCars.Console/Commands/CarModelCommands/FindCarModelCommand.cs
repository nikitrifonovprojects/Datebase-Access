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
    [CommandRoute("get CarModel")]
    public class FindCarModelCommand : BaseCommand<FindCarModelModel>
    {
        private IService<CarModel> service;
        private IMappingService mapping;

        public FindCarModelCommand(CommandContext context, IService<CarModel> service, IValidator validation, IMappingService mapping) 
            : base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(FindCarModelModel item)
        {
            if (this.context.ModelState.HasError)
            {
                return this.Error(this.context.ModelState.ToString());
            }

            CarModel carModel = this.mapping.Map<CarModel>(item);

            CarModel model = this.service.GetById(carModel.ID);
            if (model == null)
            {
                return this.Error($"CarModel with ID: {model.ID} not found");
            }

            FindCarModelModel result = this.mapping.Map<FindCarModelModel>(model);

            return this.Success(result);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
