using System.Collections.Generic;
using NikiCars.Console.Input;
using NikiCars.Console.Interfaces;
using NikiCars.Data.Models;
using NikiCars.Services;

namespace NikiCars.Console.Commands
{
    public class CarCommands
    {
        private CommandContext context;
        public List<ICommand> commands { get; private set; }

        public CarCommands()
        {
            this.commands = new List<ICommand>();
        }

        public void AddContext(CommandContext con)
        {
            this.context = con;
        }

        public void AddCommand()
        {
            switch (this.context.CommandText)
            {
                case "addCarCoupe":
                    this.commands.Add(new CreateCarCoupe(this.context, DependencyContainer.Resolve<IService<CarCoupe>>()));
                    break;
                case "addCarMake":
                    this.commands.Add(new CreateCarMake(this.context, DependencyContainer.Resolve<IService<CarMake>>()));
                    break;
                default:
                    break;
            }
        }
    }
}
