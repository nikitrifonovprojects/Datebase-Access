using AutoMapper;
using NikiCars.Data.Models;
using NikiCars.Models.CarModels;

namespace NikiCars.Services.Mapping.Profiles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<CreateCarModel, Car>(MemberList.None).ReverseMap();
            CreateMap<EditCarModel, Car>(MemberList.None).ReverseMap();
        }
    }
}
