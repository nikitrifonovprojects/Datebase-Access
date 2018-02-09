using System;
using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Console.Constants;
using NikiCars.Data.Models;
using NikiCars.Services.Interfaces;

namespace NikiCars.Console.Commands
{
    [CommandRoute("edit User")]
    public class EditUserCommand : BaseCommand<User>
    {
        private IUserService service;

        public EditUserCommand(CommandContext context, IUserService service, IModelBinder<User> binder, IValidator validation) 
            : base(context, binder, validation)
        {
            this.service = service;
        }

        protected override ICommandResult ExecuteAction(User item)
        {
            if (!this.context.CommandUser.IsAuthenticated)
            {
                return this.AuthenticationError("User not logged in");
            }

            if (item.ID != Convert.ToInt32(this.context.CommandUser.ID) || !this.context.CommandUser.UserRoles.Contains(RoleConstants.ADMINISTRATOR))
            {
                return this.AuthorizationError("User does not have permission");
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
    }
}
