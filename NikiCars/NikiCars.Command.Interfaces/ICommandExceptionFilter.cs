namespace NikiCars.Command.Interfaces
{
    public interface ICommandExceptionFilter
    {
        void OnException(ExceptionContext context);
    }
}
