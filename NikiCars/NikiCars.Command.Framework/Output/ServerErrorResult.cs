namespace NikiCars.Command.Framework.Output
{
    public class ServerErrorResult<T> : BaseCommandResult<T>
    {
        private const string STATUS_SERVER_ERROR = "Server error";

        public ServerErrorResult()
            :base()
        {

        }

        public ServerErrorResult(T item) 
            : base(item)
        {

        }

        protected override string GetStatus()
        {
            return STATUS_SERVER_ERROR;
        }
    }
}
