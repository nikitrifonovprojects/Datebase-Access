namespace NikiCars.Command.Interfaces
{
    public interface ICommandResultExecutingFilter
    {
        void OnResultExecuting(CommandContext context);
    }
}
