using System.Collections.Generic;
using NikiCars.Data.Models;
using NikiCars.Data.Models.Includes;

namespace NikiCars.Data.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByName(string loginName, List<UserIncludes> list = null);

        User GetUserByEmail(string email, List<UserIncludes> list = null);

        User GetUserByMobilePhone(string phone);
    }
}
