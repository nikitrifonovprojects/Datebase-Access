using System;
using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Attributes;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Console.Constants;
using NikiCars.Data.Models;
using NikiCars.Models.CarModels;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.CarCommands
{
    [Authorization]
    [Validate]
    [CommandRoute("edit Car")]
    public class EditCarCommand : BaseCommand<EditCarModel>
    {
        IService<Car> service;
        IMappingService mapping;

        public EditCarCommand(CommandContext context, IService<Car> service,  IValidator validation, IMappingService mapping) 
            : base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(EditCarModel item)
        {
            Car dbCar = this.service.GetById(item.ID);

            if (dbCar == null)
            {
                return this.Error(item.ID);
            }
           
            if (dbCar.UserID != Convert.ToInt32(this.context.CommandUser.ID) && !this.context.CommandUser.UserRoles.Contains(RoleConstants.ADMINISTRATOR))
            {
                return this.AuthorizationError();
            }

            dbCar = this.mapping.Map(item, dbCar);

            Car savedCar = this.service.Save(dbCar);

            EditCarModel model = this.mapping.Map<EditCarModel>(savedCar);

            return this.Success(model);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
