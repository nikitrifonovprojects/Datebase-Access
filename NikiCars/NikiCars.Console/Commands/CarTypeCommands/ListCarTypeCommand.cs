﻿using System.Collections.Generic;
using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Data.Models;
using NikiCars.Models.CarTypeModels;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.CarTypeCommands
{
    [CommandRoute("list CarType")]
    public class ListCarTypeCommand : BaseCommand<ListCarTypeModel>
    {
        private IService<CarType> service;
        private IMappingService mapping;

        public ListCarTypeCommand(CommandContext context, IService<CarType> service, IModelBinder<ListCarTypeModel> binder, IValidator validation, IMappingService mapping) 
            : base(context, binder, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(ListCarTypeModel item)
        {
            List<CarType> list;
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
                List<ListCarTypeModel> catTypeList = new List<ListCarTypeModel>();
                foreach (var carType in list)
                {
                    catTypeList.Add(this.mapping.Map<ListCarTypeModel>(carType));
                }

                return this.Success(catTypeList);
            }
            else
            {
                return this.Error($"No CarType found");
            }
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}