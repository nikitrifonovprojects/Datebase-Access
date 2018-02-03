﻿using NikiCars.Console.Input;
using NikiCars.Console.Interfaces;
using NikiCars.Console.Routing;
using NikiCars.Console.Validation;
using NikiCars.Data.Models;
using NikiCars.Services;

namespace NikiCars.Console.Commands
{
    [CommandRoute("register User")]
    public class CreateUserCommand : BaseCommand<User>
    {
        private IUserService service;

        public CreateUserCommand(CommandContext context, IUserService service, IModelBinder<User> binder, IValidator validation) 
            : base(context, binder, validation)
        {
            this.service = service;
        }

        protected override ICommandResult ExecuteAction(User item)
        {
            if (this.context.ModelState.HasError)
            {
                return this.Error(this.context.ModelState.ToString());
            }

            if (this.service.LoginNameExists(item.LoginName))
            {
                return this.Error($"LoginName {item.LoginName} already exists");
            }

            if (this.service.EmailExists(item.Email))
            {
                return this.Error($"Email {item.Email} already exists");
            }

            if (this.service.MobilePhoneExists(item.MobilePhone))
            {
                return this.Error($"MobilePhone {item.MobilePhone} already exists");
            }

            User result = this.service.Save(item);

            return this.Success(result);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}