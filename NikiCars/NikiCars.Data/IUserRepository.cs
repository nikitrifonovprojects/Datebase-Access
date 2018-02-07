using System.Collections.Generic;
using NikiCars.Data.Includes;
using NikiCars.Data.Models;

namespace NikiCars.Data
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByName(string loginName, List<UserIncludes> list = null);

        User GetUserByEmail(string email, List<UserIncludes> list = null);

        User GetUserByMobilePhone(string phone);
        
    }
}
