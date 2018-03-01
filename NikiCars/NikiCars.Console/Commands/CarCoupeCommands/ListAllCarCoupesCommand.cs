﻿using System.Collections.Generic;
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
    public class ListAllCarCoupesCommand : BaseCommand<ListAllCarCoupesModel>
    {
        private IService<CarCoupe> service;
        private IMappingService mapping;

        public ListAllCarCoupesCommand(CommandContext context, IService<CarCoupe> service, IValidator validation, IMappingService mapping)
            : base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(ListAllCarCoupesModel item)
        {
            List<CarCoupe> list;
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
                List<ListAllCarCoupesModel> carCoupelist = new List<ListAllCarCoupesModel>();
                foreach (var carCoupe in list)
                {
                    carCoupelist.Add(this.mapping.Map<ListAllCarCoupesModel>(carCoupe));
                }

                return this.Success(carCoupelist);
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
