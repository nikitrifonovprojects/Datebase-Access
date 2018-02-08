using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Newtonsoft.Json;
using NikiCars.Console.Authentication;
using NikiCars.Console.CommandClients;
using NikiCars.Console.Commands;
using NikiCars.Console.Input;
using NikiCars.Command.Interfaces;
using NikiCars.Console.ModelBinders;
using NikiCars.Console.Utility;
using NikiCars.Command.Validation;
using NikiCars.Data;
using NikiCars.Data.Models;
using NikiCars.Services;
using System.Reflection;
using NikiCars.Console.Routing;
using System.Data.SqlClient;
using System.Data;

namespace NikiCars.Console
{
    class Program
    {
        static void Main()
        {
            DependencyContainer.RegisterDependencies();
            //string command = "login User";
            //User user = new User();
            //user.Password = "gogotorulzz11";
            //user.LoginName = "Gogoto";

            //var client = new CommandClient(DependencyContainer.Resolve<Invoker>());
            //var result = client.SendRequest(command, user);

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
