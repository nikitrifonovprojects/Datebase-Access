using AutoMapper;
using NikiCars.Data.Models;
using NikiCars.Models.CarTypeModels;

namespace NikiCars.Services.Mapping.Profiles
{
    public class CarTypeModelsProfile : Profile
    {
        public CarTypeModelsProfile()
        {
            CreateMap<CreateCarTypeModel, CarType>(MemberList.None);
            CreateMap<EditCarTypeModel, CarType>(MemberList.None);
        }
    }
}
