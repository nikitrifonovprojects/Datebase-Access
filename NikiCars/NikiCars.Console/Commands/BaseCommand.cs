using NikiCars.Console.Input;
using NikiCars.Console.Interfaces;
using NikiCars.Console.Output;
using NikiCars.Data.Models;
using NikiCars.Services;

namespace NikiCars.Console.Commands
{
    public abstract class BaseCommand<T> : ICommand where T : IIdentifiable
    {
        protected IService<T> service;
        protected CommandContext context;
        private IModelBinder<T> binder;

        public BaseCommand(CommandContext context, IService<T> service, IModelBinder<T> binder)
        {
            this.service = service;
            this.context = context;
            this.binder = binder;
        }

        public void Dispose()
        {
            this.service.Dispose();
        }

        public string Execute()
        {
            T item = this.binder.BindModel(this.context.Properties);
            
            ICommandResult commandResult = ExecuteAction(item);
            string result = commandResult.ExecuteResult();

            return result;
        }

        protected abstract ICommandResult ExecuteAction(T item);

        protected ICommandResult Error<T2>(string obj, T2 item)
        {
            return new ErrorResult<T2>(obj, item);
        }

        protected ICommandResult Success<T2>(string obj, T2 item)
        {
            return new SuccessResult<T2>(obj, item);
        }
    }
}
