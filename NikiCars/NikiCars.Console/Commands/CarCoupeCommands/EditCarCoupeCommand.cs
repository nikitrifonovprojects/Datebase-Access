using System;
using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Console.Constants;
using NikiCars.Data.Models;
using NikiCars.Models.CarCoupeModels;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.CarCoupeCommands
{
    [CommandRoute("edit CarCoupe")]
    public class EditCarCoupeCommand : BaseCommand<EditCarCoupeModel> 
    {
        private IService<CarCoupe> service;
        private IMappingService mapping;

        public EditCarCoupeCommand(CommandContext context, IService<CarCoupe> service, IModelBinder<EditCarCoupeModel> binder, IValidator validation, IMappingService mapping) 
            : base(context, binder, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(EditCarCoupeModel item)
        {
            if (!this.context.CommandUser.IsAuthenticated)
            {
                return this.AuthenticationError("User not logged in");
            }

            CarCoupe carCoupe = this.mapping.Map<CarCoupe>(item);

            if (carCoupe.UserID != Convert.ToInt32(this.context.CommandUser.ID) || !this.context.CommandUser.UserRoles.Contains(RoleConstants.ADMINISTRATOR))
            {
                return this.AuthorizationError("User does not have permission");
            }

            CarCoupe result = this.service.Save(carCoupe);

            return this.Success(result);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }

        
    }
}
