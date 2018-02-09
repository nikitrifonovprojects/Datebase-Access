namespace NikiCars.Command.Framework
{
    public interface IInvoker
    {
        string ExecuteCommand(string input);
    }
}