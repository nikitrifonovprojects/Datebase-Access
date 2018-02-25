namespace NikiCars.Command.Interfaces
{
    public interface ICommandActionExecutedFilter
    {
        void OnActionExecuted(CommandContext context);
    }
}
