namespace NikiCars.Command.Interfaces
{
    public interface ICommandAuthorizationFilter
    {
        void OnAuthorize(CommandContext context);
    }
}
