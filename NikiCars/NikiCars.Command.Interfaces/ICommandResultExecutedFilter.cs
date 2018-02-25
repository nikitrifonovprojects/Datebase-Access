namespace NikiCars.Command.Interfaces
{
    public interface ICommandResultExecutedFilter
    {
        void OnResultExecuted(CommandContext context);
    }
}
