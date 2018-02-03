using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NikiCars.Console.Authentication;
using NikiCars.Console.CommandClients;
using NikiCars.Console.Commands;
using NikiCars.Console.Input;
using NikiCars.Console.Interfaces;
using NikiCars.Console.ModelBinders;
using NikiCars.Console.Validation;
using NikiCars.Data;
using NikiCars.Data.Models;
using NikiCars.Services;

namespace NikiCars.Console
{
    class Program
    {
        static void Main()
        {
            DependencyContainer.RegisterDependencies();
            //Command string will be in format: "command: add CarCoupe;data:{json}"
            string command = "login User";
            User user = new User();
            user.Password = "123456";
            user.LoginName = "Pesho11";

            var client = new CommandClient(DependencyContainer.Resolve<Invoker>());
            var result = client.SendRequest(command, user);

            //prop.Add("LoginName", "Pesho11");
            //prop.Add("Password", "123456");

            //var json = JsonConvert.SerializeObject(prop);

            //var stri = $"command: add CarMake;data:{json}";

            //var invoker = DependencyContainer.Resolve<Invoker>();
            //var result = invoker.ExecuteCommand(stri);

            //int count = result.IndexOf(@"Data: ");
            //var token = result.Substring(count + 7);

            ////var ass = new DefaultModelBinder<Car>();
            ////var res = ass.BindModel(prop);

            ////string command = "get,CarCoupe,ID=^,8";
            //var parser = new Parser();
            //var res = parser.ParseCommand(stri);

            //string input = "add CarMake,Name=Niva,Country=Russia";
            //string input1 = "add CarCoupe,Name=Hummer";
            //string input2 = "get CarCoupe,ID=1";

            //var invoker = DependencyContainer.Resolve<Invoker>();
            //string result = invoker.ExecuteCommand(input);
            //System.Console.WriteLine(result);
        }
    }
}
