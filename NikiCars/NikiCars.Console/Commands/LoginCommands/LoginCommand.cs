using System.Collections.Generic;
using System.Linq;
using NikiCars.Authentication;
using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Data.Models;
using NikiCars.Data.Models.Includes;
using NikiCars.Models.LoginModels;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.LoginCommands
{
    [CommandRoute("login User")]
    public class LoginCommand : BaseCommand<LoginModel>
    {
        private IUserService service;
        private IAuthenticationManager manager;
        private IMappingService mapping;

        public LoginCommand(CommandContext context, IUserService service, IValidator validation, IAuthenticationManager manager, IMappingService mapping)
            : base(context, validation)
        {
            this.service = service;
            this.manager = manager;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(LoginModel item)
        {
            if (this.context.ModelState.HasError)
            {
                return this.Error(this.context.ModelState.ToString());
            }

            User user = null;
            User convertedUser = this.mapping.Map<User>(item);

            if (convertedUser.LoginName != null)
            {
                user = this.service.GetUserByLoginName(convertedUser.LoginName, convertedUser.Password, new List<UserIncludes>() { UserIncludes.UserRoles } );
                if (user == null)
                {
                    return this.Error($"User not found");
                }
            }
            else if (convertedUser.Email != null)
            {
                user = this.service.GetUserByEmail(convertedUser.Email, convertedUser.Password, new List<UserIncludes>() { UserIncludes.UserRoles } );
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
