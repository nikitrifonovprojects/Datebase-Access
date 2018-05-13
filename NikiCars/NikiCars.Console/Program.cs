using System;
using System.Collections.Generic;
using System.IO;
using DependencyExtentions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NikiCars.Command.Client;
using NikiCars.Data.Models;
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
using NikiCars.Search.Interfaces;
using NikiCars.Services.Interfaces;

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

            string command = "list AdvancedList";
            var input = new AdvancedSearchCarModel();
            input.CarGearbox = 1;
            input.CarMakeIds = new List<int> { 2, 1, 3, 4 };
            input.CarModelIds = new List<int> { 2, 1 };
            input.FromCarPrice = 500;
            input.ToCarPrice = 50000;
            input.ColourID = 1;
            input.FromHorsePower = 90;
            input.Kilometers = 1000000;
            input.IncludedExtrasIds = new List<int>() { 1 };
            input.IsUsed = true;
            input.IsForParts = false;
            input.OrderBy = CarOrderByEnum.Kilometers;

            var res = client.SendRequest(command, input);
            var data = res.Data as JArray;
            var real = data.ToObject<List<AdvancedSearchReturnModel>>();

            //string command1 = "get CarDetails";
            //var input1 = new ShowCarDetailsModel();
            //input1.ID = 7;

            //var res1 = client.SendRequest(command1, input1);
            //var data1 = res1.Data as JArray;
            //var real1 = data1.ToObject<List<Car>>();

            string commanda = "get CarModelByCarMake";

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
