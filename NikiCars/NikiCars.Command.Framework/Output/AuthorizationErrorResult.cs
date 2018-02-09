namespace NikiCars.Command.Framework.Output
{
    public class AuthorizationErrorResult<T> : BaseCommandResult<T>
    {
        private const string STATUS_AUTHORIZATION_ERROR = "Authorization error";

        public AuthorizationErrorResult()
        {

        }

        public AuthorizationErrorResult(T item) 
            : base(item)
        {

        }

        protected override string GetStatus()
        {
            return STATUS_AUTHORIZATION_ERROR;
        }
    }
}
