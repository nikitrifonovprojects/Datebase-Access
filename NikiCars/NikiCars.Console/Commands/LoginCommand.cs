using System.Collections.Generic;
using System.Linq;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Console.Authentication;
using NikiCars.Console.Routing;
using NikiCars.Data.Models;
using NikiCars.Data.Models.Includes;
using NikiCars.Services.Interfaces;

namespace NikiCars.Console.Commands
{
    [CommandRoute("login User")]
    public class LoginCommand : BaseCommand<User>
    {
        private IUserService service;
        private IAuthenticationManager manager;

        public LoginCommand(CommandContext context, IUserService service, IModelBinder<User> binder, IValidator validation, IAuthenticationManager manager)
            : base(context, binder, validation)
        {
            this.service = service;
            this.manager = manager;
        }

        protected override ICommandResult ExecuteAction(User item)
        {
            User user = null;

            if (item.LoginName != null)
            {
                user = this.service.GetUserByLoginName(item.LoginName, item.Password, new List<UserIncludes>() { UserIncludes.UserRoles } );
                if (user == null)
                {
                    return this.Error($"User not found");
                }
            }
            else if (item.Email != null)
            {
                user = this.service.GetUserByEmail(item.Email, item.Password, new List<UserIncludes>() { UserIncludes.UserRoles } );
                if (user == null)
                {
                    return this.Error($"User not found");
                }
            }
            
            string token = this.manager.CreateTokenString(CreateCommandUser(user));

            return this.Success(token);
        }

        private ICommandUser CreateCommandUser(User item)
        {
            ICommandUser commandUser = new CommandUser();
            commandUser.ID = item.ID.ToString();
            commandUser.IsAuthenticated = true;
            commandUser.Username = item.Name;
            commandUser.UserData = item;
            commandUser.UserRoles.AddRange(item.Roles.Select(x => x.RoleName));

            return commandUser;
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
