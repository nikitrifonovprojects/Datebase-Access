namespace NikiCars.Command.Interfaces
{
    public interface IAuthenticationManager
    {
        string CreateTokenString(ICommandUser user);

        ICommandUser GetCommandUser(string token);
    }
}
