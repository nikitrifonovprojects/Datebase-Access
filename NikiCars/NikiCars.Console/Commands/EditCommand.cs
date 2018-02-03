using System;
using NikiCars.Console.Input;
using NikiCars.Console.Interfaces;
using NikiCars.Console.Routing;
using NikiCars.Console.Validation;
using NikiCars.Data.Models;
using NikiCars.Services;

namespace NikiCars.Console.Commands
{
    [CommandRoute("edit CarCoupe")]
    public class EditCommand : BaseCommand<CarCoupe> 
    {
        private IService<CarCoupe> service;

        public EditCommand(CommandContext context, IService<CarCoupe> service, IModelBinder<CarCoupe> binder, IValidator validation) 
            : base(context, binder, validation)
        {
            this.service = service;
        }

        protected override ICommandResult ExecuteAction(CarCoupe item)
        {
            if (!this.context.CommandUser.IsAuthenticated)
            {
                return this.AuthenticationError("User not logged in");
            }

            if (item.UserID != Convert.ToInt32(this.context.CommandUser.ID))
            {
                return this.AuthorizationError("User did not create the item");
            }

            CarCoupe result = this.service.Save(item);

            return this.Success(result);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }

        
    }
}
