namespace NikiCars.Command.Interfaces
{
    public interface IParser
    {
        CommandContext ParseCommand(string input);
    }
}
