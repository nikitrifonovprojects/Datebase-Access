namespace NikiCars.Console.Output
{
    public class SuccessResult<T> : BaseCommandResult<T>
    {
        private const string STATUS_SUCCESS = "Status: Success -";

        public SuccessResult()
            : base()
        {

        }

        public SuccessResult(T item) 
            : base(item)
        {
            
        }

        protected override string StatusMessage()
        {
            return STATUS_SUCCESS;
        }
    }
}
