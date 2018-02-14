using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Console.Constants;
using NikiCars.Data.Models;
using NikiCars.Models.UserRoleModels;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.UserRoleCommands
{
    [CommandRoute("edit UserRole")]
    public class EditUserRoleCommand : BaseCommand<EditUserRoleModel>
    {
        private IService<UserRole> service;
        private IMappingService mapping;

        public EditUserRoleCommand(CommandContext context, IService<UserRole> service, IModelBinder<EditUserRoleModel> binder, IValidator validation, IMappingService mapping) 
            : base(context, binder, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(EditUserRoleModel item)
        {
            if (!this.context.CommandUser.IsAuthenticated && !this.context.CommandUser.UserRoles.Contains(RoleConstants.ADMINISTRATOR))
            {
                return this.AuthenticationError();
            }

            if (this.context.ModelState.HasError)
            {
                return this.Error(this.context.ModelState.ToString());
            }

            UserRole userRole = this.mapping.Map<UserRole>(item);

            UserRole result = this.service.Save(userRole);

            return this.Success(result);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
