namespace NikiCars.Command.Interfaces
{
    public interface ICommandActionExecutingFilter
    {
        void OnActionExecuting(CommandContext context);
    }
}
