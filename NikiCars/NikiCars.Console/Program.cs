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

namespace NikiCars.Console
{
    class Program
    {
        static void Main()
        {
            DependencyContainer.RegisterDependencies();
            string command = "login User";
            User user = new User();
            user.Password = "gogotorulzz11";
            user.LoginName = "Gogoto";

            var client = new CommandClient(DependencyContainer.Resolve<Invoker>());
            var result = client.SendRequest(command, user);

            User usera = new User();
            //usera.Address = "Stamboliiski";
            //usera.CityID = 1;
            //usera.Email = "aasdd@gnam.xom";
            //usera.IsOfficialImporter = false;
            //usera.IsOrganisation = false;
            //usera.LoginName = "Gogoto";
            //usera.MobilePhone = "07726188893";
            //usera.Name = "Gogo";
            //usera.Password = "gogotorulzz11";
            //usera.Type = "usera";

            //string newCommand = "register User";
            //var res = client.SendRequest(newCommand, usera);

            //int a = 1;
        }
    }
}
