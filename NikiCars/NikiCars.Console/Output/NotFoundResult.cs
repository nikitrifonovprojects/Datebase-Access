namespace NikiCars.Console.Output
{
    public class NotFoundResult<T> : BaseCommandResult<T>
    {
        private const string NOT_FOUND = "Not Found";

        public NotFoundResult() 
            : base()
        {

        }

        protected override string GetStatus()
        {
            return NOT_FOUND;
        }
    }
}
