namespace NikiCars.Services
{
    public interface ICryptographyService
    {
        string HashPassword(string password);

        bool ValidatePassword(string password, string hashedPassword);
    }
}
