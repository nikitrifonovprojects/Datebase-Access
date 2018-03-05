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
    [Authorization]
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
            User dbUser = this.service.GetById(item.ID);
            if (dbUser == null)
            {
                return this.Error(item.ID);
            }

            if ((item.ID != Convert.ToInt32(this.context.CommandUser.ID) && !this.context.CommandUser.UserRoles.Contains(RoleConstants.ADMINISTRATOR)))
            {
                return this.AuthorizationError();
            }

            if (item.LoginName != dbUser.LoginName && this.service.LoginNameExists(item.LoginName))
            {
                return this.Error($"LoginName {item.LoginName} already exists");
            }

            if (item.Email != dbUser.Email && this.service.EmailExists(item.Email))
            {
                return this.Error($"Email {item.Email} already exists");
            }

            if (item.MobilePhone != dbUser.MobilePhone && this.service.MobilePhoneExists(item.MobilePhone))
            {
                return this.Error($"MobilePhone {item.MobilePhone} already exists");
            }

            dbUser = this.mapping.Map(item, dbUser);
            User user = this.service.Save(dbUser);

            EditUserModel result = this.mapping.Map<EditUserModel>(user);

            return this.Success(result);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
