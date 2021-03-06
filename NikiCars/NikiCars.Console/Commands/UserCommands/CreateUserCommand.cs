﻿using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Attributes;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Data.Models;
using NikiCars.Models.UserModels;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.UserCommands
{
    [Validate]
    [CommandRoute("register User")]
    public class CreateUserCommand : BaseCommand<EditUserModel>
    {
        private IUserService service;
        private ICryptographyService cryptography;
        private IMappingService mapping;

        public CreateUserCommand(CommandContext context, IUserService service, IValidator validation, ICryptographyService cryptography, IMappingService mapping) 
            : base(context, validation)
        {
            this.service = service;
            this.cryptography = cryptography;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(EditUserModel item)
        {
            User user = this.mapping.Map<User>(item);

            if (this.service.LoginNameExists(user.LoginName))
            {
                return this.Error($"LoginName {user.LoginName} already exists");
            }

            if (this.service.EmailExists(user.Email))
            {
                return this.Error($"Email {user.Email} already exists");
            }

            if (this.service.MobilePhoneExists(user.MobilePhone))
            {
                return this.Error($"MobilePhone {user.MobilePhone} already exists");
            }

            user.Password = this.cryptography.HashPassword(user.Password);
            User result = this.service.Save(user);

            EditUserModel userModel = this.mapping.Map<EditUserModel>(result);

            return this.Success(userModel);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
