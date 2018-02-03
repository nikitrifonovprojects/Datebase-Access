using NikiCars.Data.Models;

namespace NikiCars.Data
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByName(string loginName);

        User GetUserByEmail(string email);

        User GetUserByMobilePhone(string phone);
    }
}
