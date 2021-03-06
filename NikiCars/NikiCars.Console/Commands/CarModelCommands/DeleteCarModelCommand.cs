﻿using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Attributes;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Console.Constants;
using NikiCars.Data.Models;
using NikiCars.Models.CarModelModels;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.CarModelCommands
{
    [Validate]
    [Authorization(RoleConstants.ADMINISTRATOR)]
    [CommandRoute("delete CarModel")]
    public class DeleteCarModelCommand : BaseCommand<EditCarModelModel>
    {
        private IService<CarModel> service;
        private IMappingService mapping;

        public DeleteCarModelCommand(CommandContext context, IService<CarModel> service, IValidator validation, IMappingService mapping) 
            : base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(EditCarModelModel item)
        {
            CarModel carModel = this.mapping.Map<CarModel>(item);

            if (this.service.Delete(carModel))
            {
                return this.Success(carModel);
            }

            return this.Error();
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
