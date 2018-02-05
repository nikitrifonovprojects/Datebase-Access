using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;
using NikiCars.Console.Output;

namespace NikiCars.Console.Commands
{
    public abstract class BaseCommand<T> : ICommand where T : class
    {
        protected CommandContext context;
        private IModelBinder<T> binder;
        private IValidator validator;

        public BaseCommand(CommandContext context, IModelBinder<T> binder, IValidator validation)
        {
            this.context = context;
            this.binder = binder;
            this.validator = validation;
        }

        public virtual void Dispose()
        {
            
        }

        public string Execute()
        {
            T item = this.binder.BindModel(this.context.Properties);
            this.context.ModelState = this.validator.ValidateEntity(item);

            ICommandResult commandResult = ExecuteAction(item);
            string result = commandResult.ExecuteResult();

            return result;
        }

        protected abstract ICommandResult ExecuteAction(T item);

        protected ICommandResult Error<T2>(T2 item)
        {
            return new ErrorResult<T2>(item);
        }

        protected ICommandResult Error<T2>()
        {
            return new ErrorResult<T2>();
        }

        protected ICommandResult Success<T2>()
        {
            return new SuccessResult<T2>();
        }

        protected ICommandResult Success<T2>(T2 item)
        {
            return new SuccessResult<T2>(item);
        }

        protected ICommandResult NotFound<T2>()
        {
            return new NotFoundResult<T2>();
        }

        protected ICommandResult ServerError<T2>()
        {
            return new ServerErrorResult<T2>();
        }

        protected ICommandResult AuthorizationError<T2>()
        {
            return new AuthorizationErrorResult<T2>();
        }

        protected ICommandResult AuthorizationError<T2>(T2 item)
        {
            return new AuthorizationErrorResult<T2>(item);
        }

        protected ICommandResult AuthenticationError<T2>()
        {
            return new AuthenticationErrorResult<T2>();
        }

        protected ICommandResult AuthenticationError<T2>(T2 item)
        {
            return new AuthenticationErrorResult<T2>(item);
        }
    }
}
