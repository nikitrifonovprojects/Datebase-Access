using AutoMapper;
using NikiCars.Data.Models;
using NikiCars.Models.CarDoorModels;

namespace NikiCars.Services.Mapping.Profiles
{
    public class CarDoorModelsProfile : Profile
    {
        public CarDoorModelsProfile()
        {
            CreateMap<CreateCarDoorModel, NumberOfDoors>();
            CreateMap<EditCarDoorsModel, NumberOfDoors>();
            CreateMap<ListAllCarDoorsModel, NumberOfDoors>();
        }
    }
}
