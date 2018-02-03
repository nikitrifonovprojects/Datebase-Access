namespace NikiCars.Console.Output
{
    public class AuthenticationErrorResult<T> : BaseCommandResult<T>
    {
        private const string STATUS_AUTHENTICATION_ERROR = "Authentication error";

        public AuthenticationErrorResult()
        {

        }

        public AuthenticationErrorResult(T item) 
            : base(item)
        {

        }

        protected override string GetStatus()
        {
            return STATUS_AUTHENTICATION_ERROR;
        }
    }
}
