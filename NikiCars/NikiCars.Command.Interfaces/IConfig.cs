namespace NikiCars.Command.Interfaces
{
    public interface IConfig
    {
        string SigningKey { get; }

        string SecutityKey { get; }

        string ConnectionString { get; }
    }
}
