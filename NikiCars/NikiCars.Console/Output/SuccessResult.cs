namespace NikiCars.Console.Output
{
    public class SuccessResult<T> : BaseCommandResult<T>
    {
        private const string STATUS_SUCCESS = "Success";

        public SuccessResult()
            : base()
        {

        }

        public SuccessResult(T item) 
            : base(item)
        {
            
        }

        protected override string GetStatus()
        {
            return STATUS_SUCCESS;
        }
    }
}
