using DependencyExtentions;
using NikiCars.Command.Client;
using NikiCars.Data.Models;
using NikiCars.Dependancy;
using Newtonsoft.Json.Linq;

namespace NikiCars.Console
{
    class Program
    {
        static void Main()
        {
            var container = new DependencyContainer();
            container.AddDependencies();

            //string command = "list";
            //var search = new Search();
            //search.PageNumber = 1;
            //search.PageSize = 10;

            //var client = container.Resolve<CommandClient>();
            //var result = client.SendRequest(command, search);
            var client = container.Resolve<CommandClient>();
            string comm = "login User";

            User user1 = new User();
            user1.Password = "gogotorulzz11";
            user1.LoginName = "Gogoto";

            var firstRes = client.SendRequest(comm, user1);
            if (firstRes.Status == "Success")
            {
                client.SetToken(firstRes.Data as string);
            }

            string command = "get User";
            User user = new User();
            user.ID = 12;
            var result = client.SendRequest(command, user);
            var data = result.Data as JObject;
            var real = data.ToObject<User>();

            string command1 = "edit User";
            var res = client.SendRequest(command1, real);
            //var recievedUser = JsonConvert.DeserializeObject<User>(result.Data.ToString());
            //User usera = new User();
            //usera.LoginName = "Niki";
            //usera.Password = "asmahsgt32";
        }
    }
}
