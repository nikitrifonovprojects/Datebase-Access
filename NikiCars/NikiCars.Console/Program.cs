using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NikiCars.Console.Authentication;
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

            var prop = new Dictionary<string, string>();
            //prop.Add("Name", "Kon");
            //prop.Add("LoginName", "Pesho11");
            //prop.Add("Password", "123456");
            //prop.Add("MobilePhone", "0877895512");
            //prop.Add("CityID", "1");
            //prop.Add("IsOrganisation", "false");
            //prop.Add("IsOfficialImporter", "false");
            //prop.Add("Address", "null");
            //prop.Add("Type", "null");
            //prop.Add("Bulstat", "null");
            //prop.Add("Website", "null");
            //prop.Add("PageName", "null");

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
