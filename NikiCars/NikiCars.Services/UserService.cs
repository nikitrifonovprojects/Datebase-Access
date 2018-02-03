using NikiCars.Data;
using NikiCars.Data.Models;

namespace NikiCars.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private IUserRepository userRepository;

        public UserService(IUserRepository repo) 
            : base(repo)
        {
            this.userRepository = repo;
        }

        public User GetUserByEmail(string email, string password)
        {
            User user =  this.userRepository.GetUserByEmail(email);
            if (user.Password == password)
            {
                return user;
            }

            return user;
        }

        public User GetUserByLoginName(string name, string password)
        {
            User user = this.userRepository.GetUserByName(name);
            if (user.Password == password)
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
