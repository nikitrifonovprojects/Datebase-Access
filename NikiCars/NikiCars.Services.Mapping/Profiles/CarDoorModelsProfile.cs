using AutoMapper;
using NikiCars.Data.Models;
using NikiCars.Models.CarDoorModels;

namespace NikiCars.Services.Mapping.Profiles
{
    public class CarDoorModelsProfile : Profile
    {
        public CarDoorModelsProfile()
        {
            CreateMap<CreateCarDoorModel, NumberOfDoors>(MemberList.None);
            CreateMap<EditCarDoorsModel, NumberOfDoors>(MemberList.None);
            CreateMap<ListAllCarDoorsModel, NumberOfDoors>(MemberList.None);
        }
    }
}
