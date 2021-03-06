﻿using System;
using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Attributes;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Console.Constants;
using NikiCars.Data.Models;
using NikiCars.Models.CarCoupeModels;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.CarCoupeCommands
{
    [Validate]
    [Authorization(RoleConstants.ADMINISTRATOR)]
    [CommandRoute("add CarCoupe")]
    public class CreateCarCoupeCommand : BaseCommand<CreateCarCoupeModel>
    {
        private IService<CarCoupe> service;
        private IMappingService mapping;

        public CreateCarCoupeCommand(CommandContext context, IService<CarCoupe> service, IValidator validation, IMappingService mapping)
            : base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(CreateCarCoupeModel item)
        {
            CarCoupe carCoupe = this.mapping.Map<CarCoupe>(item);
            carCoupe.UserID = Convert.ToInt32(this.context.CommandUser.ID);
            CarCoupe result = this.service.Save(carCoupe);
            
            return this.Success(result);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
