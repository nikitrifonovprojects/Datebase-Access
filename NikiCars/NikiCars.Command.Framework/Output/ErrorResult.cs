namespace NikiCars.Command.Framework.Output
{
    public class ErrorResult<T> : BaseCommandResult<T>
    {
        private const string STATUS_ERROR = "Error";

        public ErrorResult() 
            : base()
        {

        }

        public ErrorResult(T item) 
            : base(item)
        {
            
        }

        protected override string GetStatus()
        {
            return STATUS_ERROR;
        }
    }
}
