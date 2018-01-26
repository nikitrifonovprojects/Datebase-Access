using System;
using NikiCars.Console.Input;
using NikiCars.Console.Interfaces;
using NikiCars.Data.Models;
using NikiCars.Services;

namespace NikiCars.Console.Commands
{
    public class Invoker
    {
        private IParser parser;

        public Invoker(IParser parser)
        {
            this.parser = parser;
        }

        public string ExecuteCommand(string input)
        {
            var context = this.parser.Parse(input);
            ICommand command;
            switch (context.CommandText)
            {
                case "addCarCoupe":
                    command = new CreateCarCoupe(context, DependencyContainer.Resolve<IService<CarCoupe>>(), DependencyContainer.Resolve<IModelBinder<CarCoupe>>());
                    break;
                case "addCarMake":
                    command = new CreateCarMake(context, DependencyContainer.Resolve<IService<CarMake>>(), DependencyContainer.Resolve<IModelBinder<CarMake>>());
                    break;
                case "getCarCoupe":
                    command = new FindCarCoupe(context, DependencyContainer.Resolve<IService<CarCoupe>>(), DependencyContainer.Resolve<IModelBinder<CarCoupe>>());
                    break;
                default:
                    throw new ArgumentException("Invalid Command");
            }

            return command.Execute();
        }
    }
}
