namespace NikiCars.Services.Interfaces
{
    public interface ICryptographyService
    {
        string HashPassword(string password);

        bool ValidatePassword(string password, string hashedPassword);
    }
}
