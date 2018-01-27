using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NikiCars.Console.Commands;
using NikiCars.Console.Input;
using NikiCars.Console.Interfaces;
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
            //Command string will be in format: command,type,propertiy=value,property=value ....., escape char will be ^

            string command = "get,CarCoupe,ID=^,8";
            var parser = new Parser();
            parser.ParseCommand(command);

            //var car = new Car();
            //var user = new User();

            //var isValid = ValidationHelper.ValidateEntity(user);
            //var isValids = ValidationHelper.ValidateEntity(car);

           
            //string input = "add,CarMake,Name=Lada,Country=Russia";
            //string input1 = "add,CarCoupe,Name=Crane";
            //string input2 = "get,CarCoupe,ID=8";

            //var invoker = DependencyContainer.Resolve<Invoker>();
            //string result = invoker.ExecuteCommand(input2);
            //System.Console.WriteLine(result);

            //var par = new Parser();
            //par.ParseCommand(input);

            //var car = new CarMake();
            //car.Name = "Acura";
            //car.Country = "USA";

            //using (var service = DependencyContainer.Resolve<IService<CarMake>>())
            //{
            //    car = service.Save(car);
            //    var make = service.GetById(12);
            //    var show = service.GetAll(0, 5);
            //    var ass = service.Count();
            //    System.Console.WriteLine(ass);
            //}

            //var carCoupe = new CarCoupe();
            //carCoupe.Name = "seven";

            //List<CarCoupe> list;
            //using (var repo = new CarCoupeRepository())
            //{
            //    carCoupe = repo.Create(carCoupe);
            //    list = repo.GetAll(0, 20);
            //    var coupe = repo.GetByID(3);
            //    repo.Dispose();
            //}

            //var updateCoupe = new CarCoupe();
            //updateCoupe.CarCoupeID = 4;
            //updateCoupe.Name = "horor";

            //using (var repo = new CarCoupeRepository())
            //{
            //    repo.Update(updateCoupe);
            //}

            //var carMake = new CarMake();
            //carMake.Name = "Rover";
            //carMake.Country = "UK";

            //List<CarMake> makeList = new List<CarMake>();
            //using (var repo = new CarMakeRepository())
            //{
            //    carMake = repo.Create(carMake);
            //    makeList = repo.GetAll(0, 20);
            //    var coupe = repo.GetByID(3);
            //    repo.Dispose();
            //}

            //var updateMake = new CarMake();
            //updateMake.Name = "Seat";
            //updateMake.Country = "Spain";
            //updateMake.CarMakeID = 7;

            //using (var repo = new CarMakeRepository())
            //{
            //    repo.Update(updateMake);
            //}

            //var carType = new CarType();
            //carType.Name = "Submarine";

            //using (var repo = new CarTypeRepository())
            //{
            //    carType = repo.Create(carType);
            //    repo.Dispose();
            //}

            //var carModel = new CarModel();
            //carModel.Name = "Selantro";
            //carModel.CarMakeID = 2;
            //carModel.CarTypeID = 1;

            //using (var repo = new CarModelRepository())
            //{
            //    carModel = repo.Create(carModel);
            //    repo.Dispose();
            //}


            //Car car = new Car();
            //car.CarCoupeID = 1;
            //car.CarModelID = 2;
            //car.ColourID = 1;
            //car.DateCreated = DateTime.Now;
            //car.Discription = "Nice car";
            //car.EngineCapacity = 2000;
            //car.FirstRegistrationDate = DateTime.Today;
            //car.FuelTypeID = 1;
            //car.GearboxTypeID = 1;
            //car.HorsePower = 134;
            //car.IsDamaged = false;
            //car.IsForParts = false;
            //car.IsLeftSteering = false;
            //car.IsUsed = true;
            //car.Kilometers = 100000;
            //car.NumberOfDoorsID = 3;
            //car.Price = 5000;
            //car.Title = "My Car";
            //car.UserID = 3;

            //using (var repo = new CarRepository())
            //{
            //    car = repo.Create(car);
            //}


            //Picture pic = new Picture();
            //pic.CarID = 7;
            //pic.PictureFile = File.ReadAllBytes(@"C:\Users\Nikki\Desktop\Capture.JPG");

            //using (var repo = new PictureRepository())
            //{
            //    pic = repo.Create(pic);
            //    var savedPic = repo.GetByID(pic.PictureID);
            //    File.WriteAllBytes(@"C:\Users\Nikki\Desktop\pic.JPG", pic.PictureFile);
            //}
        }
    }
}
