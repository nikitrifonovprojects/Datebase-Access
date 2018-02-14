using NikiCars.Command.Framework.Output;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;

namespace NikiCars.Command.Framework
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

        protected ICommandResult Error()
        {
            return new ErrorResult<string>();
        }

        protected ICommandResult Success()
        {
            return new SuccessResult<string>();
        }

        protected ICommandResult Success<T2>(T2 item)
        {
            return new SuccessResult<T2>(item);
        }

        protected ICommandResult NotFound()
        {
            return new NotFoundResult<string>();
        }

        protected ICommandResult ServerError()
        {
            return new ServerErrorResult<string>();
        }

        protected ICommandResult AuthorizationError()
        {
            return new AuthorizationErrorResult<string>();
        }

        protected ICommandResult AuthorizationError<T2>(T2 item)
        {
            return new AuthorizationErrorResult<T2>(item);
        }

        protected ICommandResult AuthenticationError()
        {
            return new AuthenticationErrorResult<string>();
        }

        protected ICommandResult AuthenticationError<T2>(T2 item)
        {
            return new AuthenticationErrorResult<T2>(item);
        }
    }
}
