using System;
using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Attributes;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Console.Constants;
using NikiCars.Data.Models;
using NikiCars.Models.UserModels;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.UserCommands
{
    [Validate]
    [CommandRoute("edit User")]
    public class EditUserCommand : BaseCommand<EditUserModel>
    {
        private IUserService service;
        private IMappingService mapping;

        public EditUserCommand(CommandContext context, IUserService service, IValidator validation, IMappingService mapping) 
            : base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(EditUserModel item)
        {
            User user = this.mapping.Map<User>(item);

            if (!this.context.CommandUser.IsAuthenticated && (user.ID != Convert.ToInt32(this.context.CommandUser.ID) || !this.context.CommandUser.UserRoles.Contains(RoleConstants.ADMINISTRATOR)))
            {
                return this.AuthorizationError();
            }

            User dbUser = this.service.GetById(user.ID);

            if (user.LoginName != dbUser.LoginName && this.service.LoginNameExists(user.LoginName))
            {
                return this.Error($"LoginName {user.LoginName} already exists");
            }

            if (user.Email != dbUser.Email && this.service.EmailExists(user.Email))
            {
                return this.Error($"Email {user.Email} already exists");
            }

            if (user.MobilePhone != dbUser.MobilePhone && this.service.MobilePhoneExists(user.MobilePhone))
            {
                return this.Error($"MobilePhone {user.MobilePhone} already exists");
            }

            user = this.service.Save(user);

            EditUserModel result = this.mapping.Map<EditUserModel>(user);

            return this.Success(result);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
