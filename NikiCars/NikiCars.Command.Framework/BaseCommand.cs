using System;
using System.Collections.Generic;
using System.Linq;
using NikiCars.Command.Framework.Output;
using NikiCars.Command.Interfaces;
using NikiCars.Command.Validation;

namespace NikiCars.Command.Framework
{
    public abstract class BaseCommand<T> : ICommand where T : class
    {
        protected CommandContext context;
        private IValidator validator;

        public BaseCommand(CommandContext context, IValidator validation)
        {
            this.context = context;
            this.validator = validation;
        }

        public virtual void Dispose()
        {

        }

        protected virtual void OnAuthorize()
        {

        }

        protected virtual void OnActionExecuting()
        {

        }

        protected virtual void OnActionExecuted()
        {

        }

        protected virtual void OnResultExecuting()
        {

        }

        protected virtual void OnResultExecuted()
        {

        }

        protected virtual void OnException(ExceptionContext context)
        {

        }

        private void ExecuteOnAuthorize(CommandContext context)
        {
            this.OnAuthorize();
        }

        private void ExecuteOnActionExcuted(CommandContext context)
        {
            this.OnActionExecuted();
        }

        private void ExecuteOnActionExecuting(CommandContext context)
        {
            this.OnActionExecuting();
        }

        private void ExecuteOnResultExecuting(CommandContext context)
        {
            this.OnResultExecuting();
        }

        private void ExecuteOnResultExecuted(CommandContext context)
        {
            this.OnResultExecuted();
        }

        private List<Action<CommandContext>> GetActionPipeLine(Type type)
        {
            Attribute[] attributes = Attribute.GetCustomAttributes(type);
            var actionPipeline = new List<Action<CommandContext>>();

            var authAttributes = attributes.Where(x => typeof(ICommandAuthorizationFilter).IsAssignableFrom(x.GetType()))
                .Cast<ICommandAuthorizationFilter>()
                .Select<ICommandAuthorizationFilter, Action<CommandContext>>(c => c.OnAuthorize);

            actionPipeline.AddRange(authAttributes);
            actionPipeline.Add(this.ExecuteOnAuthorize);

            var actionExecuting = attributes.Where(x => typeof(ICommandActionExecutingFilter).IsAssignableFrom(x.GetType()))
                .Cast<ICommandActionExecutingFilter>()
                .Select<ICommandActionExecutingFilter, Action<CommandContext>>(c => c.OnActionExecuting);

            actionPipeline.Add(this.ExecuteValidate);

            actionPipeline.AddRange(actionExecuting);
            actionPipeline.Add(this.ExecuteOnActionExecuting);

            actionPipeline.Add(this.ExecuteResponseResultAction);

            return actionPipeline;
        }

        private List<Action<CommandContext>> GetResultPipeLine(Type type)
        {
            Attribute[] attributes = Attribute.GetCustomAttributes(type);
            var resultPipeline = new List<Action<CommandContext>>();

            var resultExecuting = attributes.Where(x => typeof(ICommandResultExecutingFilter).IsAssignableFrom(x.GetType()))
               .Cast<ICommandResultExecutingFilter>()
               .Select<ICommandResultExecutingFilter, Action<CommandContext>>(c => c.OnResultExecuting);

            resultPipeline.AddRange(resultExecuting);
            resultPipeline.Add(this.ExecuteOnResultExecuting);

            resultPipeline.Add(this.ExecuteResponseResult);

            var resultExecuted = attributes.Where(x => typeof(ICommandResultExecutedFilter).IsAssignableFrom(x.GetType()))
               .Cast<ICommandResultExecutedFilter>()
               .Select<ICommandResultExecutedFilter, Action<CommandContext>>(c => c.OnResultExecuted);

            resultPipeline.Add(this.ExecuteOnResultExecuted);
            resultPipeline.AddRange(resultExecuted);

            return resultPipeline;
        }

        private List<Action<CommandContext>> GetActionExecutedPipeline(Type type)
        {
            Attribute[] attributes = Attribute.GetCustomAttributes(type);
            var actionExecutedPipeline = new List<Action<CommandContext>>();

            var actionExecuted = attributes.Where(x => typeof(ICommandActionExecutedFilter).IsAssignableFrom(x.GetType()))
              .Cast<ICommandActionExecutedFilter>()
              .Select<ICommandActionExecutedFilter, Action<CommandContext>>(c => c.OnActionExecuted);

            actionExecutedPipeline.Add(this.ExecuteOnActionExcuted);
            actionExecutedPipeline.AddRange(actionExecuted);

            return actionExecutedPipeline;
        }

        private List<Action<ExceptionContext>> GetExceptionPipeLine(Type type)
        {
            Attribute[] attributes = Attribute.GetCustomAttributes(type);
            var exceptionPipeline = new List<Action<ExceptionContext>>();

            var onException = attributes.Where(x => typeof(ICommandExceptionFilter).IsAssignableFrom(x.GetType()))
              .Cast<ICommandExceptionFilter>()
              .Select<ICommandExceptionFilter, Action<ExceptionContext>>(c => c.OnException);

            exceptionPipeline.Add(this.OnException);
            exceptionPipeline.AddRange(onException);

            return exceptionPipeline;
        }

        private void ExecuteValidate(CommandContext context)
        {
            T item = (T)this.context.Properties;
            this.context.ModelState = this.validator.ValidateEntity(item);
        }

        private void ExecuteResponseResultAction(CommandContext context)
        {
            T item = (T)this.context.Properties;
            this.context.ResponseResult = ExecuteAction(item);
        }

        private void ExecuteResponseResult(CommandContext context)
        {
            this.context.ResultString = this.context.ResponseResult.ExecuteResult();
        }

        public string Execute()
        {
            var type = this.GetType();

            try
            {
                var actionPipeline = this.GetActionPipeLine(type);

                foreach (var action in actionPipeline)
                {
                    action(this.context);

                    if (this.context.ResponseResult != null)
                    {
                        break;
                    }
                }

                var actionExecutedPipeline = this.GetActionExecutedPipeline(type);

                foreach (var action in actionExecutedPipeline)
                {
                    action(this.context);
                }
            }
            catch (Exception ex)
            {
                var exceptionContext = new ExceptionContext(this.context);
                exceptionContext.IsHandled = false;
                exceptionContext.Exception = ex;

                var exceptionPipeline = GetExceptionPipeLine(type);

                foreach (var exeptionHandler in exceptionPipeline)
                {
                    exeptionHandler(exceptionContext);
                }

                if (!exceptionContext.IsHandled)
                {
                    throw;
                }
            }

            var resultPipeline = this.GetResultPipeLine(type);

            foreach (var action in resultPipeline)
            {
                action(this.context);
            }

            return this.context.ResultString;
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
