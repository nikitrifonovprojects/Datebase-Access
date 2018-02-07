using System.Collections.Generic;
using NikiCars.Data.Includes;
using NikiCars.Data.Models;

namespace NikiCars.Services
{
    public interface IUserService : IService<User>
    {
        User GetUserByLoginName(string name, string password, List<UserIncludes> list = null);

        User GetUserByEmail(string email, string password, List<UserIncludes> list = null);
       
        bool LoginNameExists(string loginName);

        bool EmailExists(string email);

        bool MobilePhoneExists(string mobilePhone);
    }
}
