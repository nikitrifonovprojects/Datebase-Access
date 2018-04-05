using System;
using System.Collections.Generic;
using System.IO;
using DependencyExtentions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NikiCars.Command.Client;
using NikiCars.Data.Revisions;
using NikiCars.Dependancy;
using NikiCars.Logging;
using NikiCars.Models.CarCoupeModels;
using NikiCars.Models.CarMakeModels;
using NikiCars.Models.CarModelModels;
using NikiCars.Models.CarModels;
using NikiCars.Models.CarTypeModels;
using NikiCars.Models.LoginModels;
using NikiCars.Models.UserModels;
using NikiCars.Search;

namespace NikiCars.Console
{
    class Program
    {

        static void Main()
        {
            var container = new DependencyContainer();
            container.AddDependencies();

            var user1 = new LoginModel();
            user1.Password = "gogotorulzz11";
            user1.LoginName = "Gogoto";
            string comm = "login User";

            var client = container.Resolve<CommandClient>();

            var firstRes = client.SendRequest(comm, user1);
            if (firstRes.Status == "Success")
            {
                client.SetToken(firstRes.Data as string);
            }

            string command = "edit Car";
            var input = new EditCarModel();
            input.ID = 30;
            input.CarCoupeID = 2;
            input.CarModelID = 2;
            input.Discription = "very good and bad car";
            input.EngineCapacity = 1600;
            input.FirstRegistrationDate = DateTime.Now.AddYears(-10);
            input.HorsePower = 100;
            input.IsDamaged = false;
            input.IsForParts = false;
            input.IsLeftSteering = false;
            input.IsUsed = true;
            input.Kilometers = 20100;
            input.NumberOfDoorsID = 2;
            input.Price = 12000;


            var res = client.SendRequest(command, input);
            //string command = "get CarModelByCarMake";

            //var carCoupe = new ListAllCarCoupesModel();

            //var carModel = new ViewCarModelsByCarMakeModel();
            //carModel.CarMakeID = 2;


            // var res = client.SendRequest(command, carModel);
            //var client = container.Resolve<CommandClient>();

            //var result = client.SendRequest(command, carCoupe);
            //var data = result.Data as JArray;
            //var real = data.ToObject<List<ListAllCarCoupesModel>>();

            //var resultWithPaging = client.SendRequest(command, carCoupeWithPaging);
            //var dataWithPaging = resultWithPaging.Data as JArray;
            //var realWithPaging = dataWithPaging.ToObject<List<ListAllCarCoupesModel>>();

            //string command = "list";
            //var search = new Search();
            //search.PageNumber = 1;
            //search.PageSize = 10;

            //var client = container.Resolve<CommandClient>();
            //var result = client.SendRequest(command, search);
            //var client = container.Resolve<CommandClient>();
            //string comm = "login User";

            //var user1 = new LoginModel();
            //user1.Password = "gogotorulzz11";
            //user1.LoginName = "Gogoto";

            //var firstRes = client.SendRequest(comm, user1);
            //if (firstRes.Status == "Success")
            //{
            //    client.SetToken(firstRes.Data as string);
            //}

            //string command = "get User";
            //var user = new FindUserModel();
            //user.ID = 12;
            //var result = client.SendRequest(command, user);
            //var data = result.Data as JObject;
            //var real = data.ToObject<FindUserModel>();

            //string command1 = "edit User";
            //var res = client.SendRequest(command1, real);

            //var recievedUser = JsonConvert.DeserializeObject<User>(result.Data.ToString());
            //User usera = new User();
            //usera.LoginName = "Niki";
            //usera.Password = "asmahsgt32";
        }
    }
}
