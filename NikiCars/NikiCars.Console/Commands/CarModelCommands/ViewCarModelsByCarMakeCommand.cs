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
    [CommandRoute("get CarModelByCarMake")]
    public class ViewCarModelsByCarMakeCommand : BaseCommand<ViewCarModelsByCarMakeModel>
    {
        private IService<CarModel> service;
        private IMappingService mapping;

        public ViewCarModelsByCarMakeCommand(CommandContext context, IService<CarModel> service, IModelBinder<ViewCarModelsByCarMakeModel> binder, IValidator validation, IMappingService mapping) 
            : base(context, binder, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(ViewCarModelsByCarMakeModel item)
        {
            List<CarModel> list = list = this.service.GetAll();

            if (list.Count > 0)
            {
                List<ViewCarModelsByCarMakeModel> carModellist = new List<ViewCarModelsByCarMakeModel>();
                foreach (var carModel in list)
                {
                    if (carModel.CarMakeID == item.CarMakeID)
                    {
                        carModellist.Add(this.mapping.Map<ViewCarModelsByCarMakeModel>(carModel));
                    }
                }

                if (carModellist.Count > 0)
                {
                    return this.Success(carModellist);
                }

                return this.Error($"No CarModel by this make found");
            }
            else
            {
                return this.Error($"No CarModel found");
            }
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
