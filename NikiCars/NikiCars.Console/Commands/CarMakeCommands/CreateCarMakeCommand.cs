﻿using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Data.Models;
using NikiCars.Models.CarMakeModels;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.CarMakeCommands
{
    [CommandRoute("add CarMake")]
    public class CreateCarMakeCommand : BaseCommand<CreateCarMakeModel>
    {
        private IService<CarMake> service;
        private IMappingService mapping;

        public CreateCarMakeCommand(CommandContext context, IService<CarMake> service, IModelBinder<CreateCarMakeModel> binder, IValidator validation, IMappingService mapping) 
            : base(context, binder, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(CreateCarMakeModel item)
        {
            if (this.context.ModelState.HasError)
            {
                return this.Error(this.context.ModelState.ToString());
            }

            if (!this.context.CommandUser.IsAuthenticated)
            {
                return this.AuthenticationError("User is not authenticated");
            }

            CarMake carMake = this.mapping.Map<CarMake>(item);

            CarMake result = this.service.Save(carMake);

            return this.Success(result);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}