namespace NikiCars.Console.Interfaces
{
    public interface IAuthenticationManager
    {
        string CreateTokenString(ICommandUser user);

        ICommandUser GetCommandUser(string token);
    }
}
