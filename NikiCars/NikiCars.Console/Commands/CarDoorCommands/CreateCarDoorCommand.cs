﻿using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Attributes;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Console.Constants;
using NikiCars.Data.Models;
using NikiCars.Models.CarDoorModels;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.CarDoorCommands
{
    [Validate]
    [Authorization(RoleConstants.ADMINISTRATOR)]
    [CommandRoute("add CarDoors")]
    public class CreateCarDoorCommand : BaseCommand<CreateCarDoorModel>
    {
        private IService<NumberOfDoors> service;
        private IMappingService mapping;

        public CreateCarDoorCommand(CommandContext context, IService<NumberOfDoors> service, IValidator validation, IMappingService mapping) 
            : base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(CreateCarDoorModel item)
        {
            NumberOfDoors model = this.mapping.Map<NumberOfDoors>(item);

            NumberOfDoors result = this.service.Save(model);

            return this.Success(result);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
