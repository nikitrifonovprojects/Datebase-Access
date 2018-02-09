using DependencyExtentions;
using NikiCars.Command.Client;
using NikiCars.Command.Framework;
using NikiCars.Command.Framework.DependencyExtensions;
using NikiCars.Command.Interfaces;
using NikiCars.Data.Models;
using NikiCars.Dependancy;

namespace NikiCars.Console
{
    class Program
    {
        static void Main()
        {
            var container = new DependencyContainer();
            container.AddDependencies();
            container.AddConsoleDependencies();
            string command = "login User";
            User user = new User();
            user.Password = "gogotorulzz11";
            user.LoginName = "Gogoto";

            var client = new CommandClient(new Invoker(container.Resolve<IParser>(), container));
            var result = client.SendRequest(command, user);

            //UserRole role;
            //using (var repo = DependencyContainer.Resolve<IRepository<UserRole>>())
            //{
            //    role = repo.GetByID(1);
            //}

            //using (var repo = DependencyContainer.Resolve<IUserRepository>())
            //{
            //    var user = repo.GetUserByName("Gogoto", new List<Data.Includes.UserIncludes> { Data.Includes.UserIncludes.UserRoles });

            //    user.Roles.Remove(role);
            //    user.Email = "nik@gnam.xom";

            //    var res = repo.Update(user);

            //    var resToCompare = repo.GetUserByName("Gogoto", new List<Data.Includes.UserIncludes> { Data.Includes.UserIncludes.UserRoles });
            //}

            //User usera = new User();
            //usera.Address = "Odrin";
            //usera.CityID = 1;
            //usera.Email = "nik@gnam.xom";
            //usera.IsOfficialImporter = false;
            //usera.IsOrganisation = false;
            //usera.LoginName = "Niki";
            //usera.MobilePhone = "07726184593";
            //usera.Name = "Nikolay";
            //usera.Password = "asmahsgt32";

            //string newCommand = "register User";
            //var res = client.SendRequest(newCommand, usera);
        }
    }
}
