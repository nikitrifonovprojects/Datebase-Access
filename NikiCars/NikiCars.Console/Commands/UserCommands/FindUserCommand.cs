using System.Collections.Generic;
using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Data.Models;
using NikiCars.Data.Models.Includes;
using NikiCars.Services.Interfaces;

namespace NikiCars.Console.Commands.UserCommands
{
    [CommandRoute("get User")]
    public class FindUserCommand : BaseCommand<User>
    {
        private IUserService service;

        public FindUserCommand(CommandContext context, IUserService service, IModelBinder<User> binder, IValidator validation) 
            : base(context, binder, validation)
        {
            this.service = service;
        }

        protected override ICommandResult ExecuteAction(User item)
        {
            User result = this.service.GetUserByID(item.ID, new List<UserIncludes> { UserIncludes.UserRoles });
            if (result == null)
            {
                return this.Error($"User with ID: {item.ID} not found");
            }

            return this.Success(result);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
