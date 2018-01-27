namespace NikiCars.Console.Output
{
    public class ErrorResult<T> : BaseCommandResult<T>
    {
        private const string STATUS_ERROR = "Status: Fail -";

        public ErrorResult() 
            : base()
        {

        }

        public ErrorResult(T item) 
            : base(item)
        {
            
        }

        protected override string StatusMessage()
        {
            return STATUS_ERROR;
        }
    }
}
