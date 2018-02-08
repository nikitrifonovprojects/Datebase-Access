using System.Collections.Generic;
using NikiCars.Data.Interfaces;
using NikiCars.Data.Models;
using NikiCars.Data.Models.Includes;
using NikiCars.Services.Interfaces;

namespace NikiCars.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private IUserRepository userRepository;
        private ICryptographyService cryptography;

        public UserService(IUserRepository repo, ICryptographyService cryptography) 
            : base(repo)
        {
            this.userRepository = repo;
            this.cryptography = cryptography;
        }

        public User GetUserByEmail(string email, string password, List<UserIncludes> list = null)
        {
            User user =  this.userRepository.GetUserByEmail(email, list);
            if (user != null && this.cryptography.ValidatePassword(password, user.Password))
            {
                return user;
            }

            return user;
        }

        public User GetUserByLoginName(string name, string password, List<UserIncludes> list = null)
        {
            User user = this.userRepository.GetUserByName(name, list);
            if (user != null && this.cryptography.ValidatePassword(password, user.Password))
            {
                return user;
            }

            return user;
        }

        public bool LoginNameExists(string loginName)
        {
            return this.userRepository.GetUserByName(loginName) != null;
        }

        public bool EmailExists(string email)
        {
            return this.userRepository.GetUserByEmail(email) != null;
        }

        public bool MobilePhoneExists(string mobilePhone)
        {
            return this.userRepository.GetUserByMobilePhone(mobilePhone) != null;
        }
    }
}
