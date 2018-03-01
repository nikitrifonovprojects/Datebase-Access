using System.Collections.Generic;
using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Attributes;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Data.Models;
using NikiCars.Data.Models.Includes;
using NikiCars.Models.UserModels;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.UserCommands
{
    [Validate]
    [CommandRoute("get User")]
    public class FindUserCommand : BaseCommand<FindUserModel>
    {
        private IUserService service;
        private IMappingService mapping;

        public FindUserCommand(CommandContext context, IUserService service, IValidator validation, IMappingService mapping)
            : base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(FindUserModel item)
        {
            User mappedUser = this.mapping.Map<User>(item);

            User user = this.service.GetUserByID(mappedUser.ID, new List<UserIncludes> { UserIncludes.UserRoles });
            if (user == null)
            {
                return this.Error($"User with ID: {user.ID} not found");
            }

            FindUserModel result = this.mapping.Map<FindUserModel>(user);

            return this.Success(result);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
