﻿using System;
using NikiCars.Console.Input;
using NikiCars.Command.Interfaces;
using NikiCars.Console.Routing;
using NikiCars.Command.Validation;
using NikiCars.Data.Models;
using NikiCars.Services;

namespace NikiCars.Console.Commands
{
    [CommandRoute("add CarCoupe")]
    public class CreateCarCoupeCommand : BaseCommand<CarCoupe>
    {
        private IService<CarCoupe> service;

        public CreateCarCoupeCommand(CommandContext context, IService<CarCoupe> service, IModelBinder<CarCoupe> binder, IValidator validation)
            : base(context, binder, validation)
        {
            this.service = service;
        }

        protected override ICommandResult ExecuteAction(CarCoupe item)
        {
            if (this.context.ModelState.HasError)
            {
                return this.Error(this.context.ModelState.ToString());
            }

            if (!this.context.CommandUser.IsAuthenticated)
            {
                return this.AuthenticationError("User is not authenticated");
            }

            item.UserID = Convert.ToInt32(this.context.CommandUser.ID);
            CarCoupe result = this.service.Save(item);
            
            return this.Success(result);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
