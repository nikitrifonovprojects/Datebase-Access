using AutoMapper;
using NikiCars.Data.Models;
using NikiCars.Models.CarModelModels;

namespace NikiCars.Services.Mapping.Profiles
{
    public class CarModelModelsProfile : Profile
    {
        public CarModelModelsProfile()
        {
            CreateMap<CreateCarModelModel, CarModel>(MemberList.None);
            CreateMap<EditCarModelModel, CarModel>(MemberList.None);
            CreateMap<ListCarModelModel, CarModel>(MemberList.None);
            CreateMap<FindCarModelModel, CarModel>(MemberList.None).ReverseMap();
        }
    }
}
