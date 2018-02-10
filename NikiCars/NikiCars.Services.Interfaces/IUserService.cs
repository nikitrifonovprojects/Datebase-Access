using System.Collections.Generic;
using NikiCars.Data.Models;
using NikiCars.Data.Models.Includes;

namespace NikiCars.Services.Interfaces
{
    public interface IUserService : IService<User>
    {
        User GetUserByLoginName(string name, string password, List<UserIncludes> list = null);

        User GetUserByEmail(string email, string password, List<UserIncludes> list = null);

        User GetUserByID(int id, List<UserIncludes> list = null);

        bool LoginNameExists(string loginName);

        bool EmailExists(string email);

        bool MobilePhoneExists(string mobilePhone);
    }
}
