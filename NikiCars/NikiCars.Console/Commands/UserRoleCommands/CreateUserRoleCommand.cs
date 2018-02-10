using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Console.Constants;
using NikiCars.Data.Models;
using NikiCars.Services.Interfaces;

namespace NikiCars.Console.Commands.UserRoleCommands
{
    [CommandRoute("add UserRole")]
    public class CreateUserRoleCommand : BaseCommand<UserRole>
    {
        private IService<UserRole> service;

        public CreateUserRoleCommand(CommandContext context, IService<UserRole> service, IModelBinder<UserRole> binder, IValidator validation) 
            : base(context, binder, validation)
        {
            this.service = service;
        }

        protected override ICommandResult ExecuteAction(UserRole item)
        {
            if (this.context.ModelState.HasError)
            {
                return this.Error(this.context.ModelState.ToString());
            }

            if (!this.context.CommandUser.IsAuthenticated || !this.context.CommandUser.UserRoles.Contains(RoleConstants.ADMINISTRATOR))
            {
                return this.AuthenticationError("User does not have permission");
            }

            UserRole result = this.service.Save(item);

            return this.Success(result);
        }
    }
}
