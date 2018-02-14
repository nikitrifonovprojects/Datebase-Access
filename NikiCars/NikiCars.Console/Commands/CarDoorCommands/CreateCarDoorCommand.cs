﻿using NikiCars.Command.Framework;
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
    [CommandRoute("add CarDoors")]
    public class CreateCarDoorCommand : BaseCommand<CreateCarDoorModel>
    {
        private IService<NumberOfDoors> service;
        private IMappingService mapping;

        public CreateCarDoorCommand(CommandContext context, IService<NumberOfDoors> service, IModelBinder<CreateCarDoorModel> binder, IValidator validation, IMappingService mapping) 
            : base(context, binder, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(CreateCarDoorModel item)
        {
            if (this.context.ModelState.HasError)
            {
                return this.Error(this.context.ModelState.ToString());
            }

            if (!this.context.CommandUser.IsAuthenticated || !this.context.CommandUser.UserRoles.Contains(RoleConstants.ADMINISTRATOR))
            {
                return this.AuthenticationError("User is not authenticated");
            }

            NumberOfDoors carMake = this.mapping.Map<NumberOfDoors>(item);

            NumberOfDoors result = this.service.Save(carMake);

            return this.Success(result);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}