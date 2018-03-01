using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Attributes;
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
    [Validate]
    [Authorization(RoleConstants.ADMINISTRATOR)]
    [CommandRoute("add UserRole")]
    public class CreateUserRoleCommand : BaseCommand<CreateUserRoleModel>
    {
        private IService<UserRole> service;
        private IMappingService mapping;

        public CreateUserRoleCommand(CommandContext context, IService<UserRole> service, IValidator validation, IMappingService mapping) 
            : base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(CreateUserRoleModel item)
        {
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
