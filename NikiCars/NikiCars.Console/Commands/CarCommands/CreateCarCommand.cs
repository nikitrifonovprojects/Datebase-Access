using System;
using NikiCars.Command.Framework;
using NikiCars.Command.Framework.Attributes;
using NikiCars.Command.Framework.Routing;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Data.Models;
using NikiCars.Models.CarModels;
using NikiCars.Services.Interfaces;
using NikiCars.Services.Mapping;

namespace NikiCars.Console.Commands.CarCommands
{
    [Validate]
    [Authorization]
    [CommandRoute("add Car")]
    public class CreateCarCommand : BaseCommand<CreateCarModel>
    {
        IService<Car> service;
        IMappingService mapping;

        public CreateCarCommand(CommandContext context, IService<Car> service, IValidator validation, IMappingService mapping) 
            : base(context, validation)
        {
            this.service = service;
            this.mapping = mapping;
        }

        protected override ICommandResult ExecuteAction(CreateCarModel item)
        {
            Car car = this.mapping.Map<Car>(item);
            car.UserID = Convert.ToInt32(this.context.CommandUser.ID);
            car.DateCreated = DateTime.Now;
            Car savedCar = this.service.Save(car);
            CreateCarModel model = this.mapping.Map<CreateCarModel>(savedCar);

            return this.Success(model);
        }

        public override void Dispose()
        {
            this.service.Dispose();
        }
    }
}
