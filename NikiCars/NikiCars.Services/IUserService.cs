using NikiCars.Data.Models;

namespace NikiCars.Services
{
    public interface IUserService : IService<User>
    {
        User GetUserByLoginName(string name, string password);

        User GetUserByEmail(string email, string password);

        bool LoginNameExists(string loginName);

        bool EmailExists(string email);

        bool MobilePhoneExists(string mobilePhone);
    }
}
