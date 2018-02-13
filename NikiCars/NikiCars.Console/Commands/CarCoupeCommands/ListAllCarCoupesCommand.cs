using System.Collections.Generic;
using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Data.Models;
using NikiCars.Models.CarCoupeModels;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.CarCoupeCommands
{
    [CommandRoute("list CarCoupe")]
    public class ListAllCarCoupesCommand : BaseCommand<ListAllCarCoupesModel>
    {
        private IService<CarCoupe> service;
        private IMappingService mapping;

        public ListAllCarCoupesCommand(CommandContext context, IService<CarCoupe> service, IModelBinder<ListAllCarCoupesModel> binder, IValidator validation, IMappingService mapping)
            : base(context, binder, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(ListAllCarCoupesModel item)
        {
            List<CarCoupe> list;
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
                List<ListAllCarCoupesModel> carCoupelist = new List<ListAllCarCoupesModel>();
                foreach (var carCoupe in list)
                {
                    carCoupelist.Add(this.mapping.Map<ListAllCarCoupesModel>(carCoupe));
                }

                return this.Success(carCoupelist);
            }
            else
            {
                return this.Error($"No CarCoupe's found");
            }
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
