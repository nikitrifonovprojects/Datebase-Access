using NikiCars.Console.Input;
using NikiCars.Console.Interfaces;
using NikiCars.Data.Models;
using NikiCars.Services;

namespace NikiCars.Console.Commands
{
    public abstract class BaseCommand<T> : ICommand where T : IIdentifiable
    {
        protected IService<T> service;
        protected CommandContext context;

        public BaseCommand(CommandContext context, IService<T> service)
        {
            this.service = service;
            this.context = context;
        }

        public void Dispose()
        {
            this.service.Dispose();
        }

        protected abstract T BindModel();

        public abstract void Execute();
    }
}
