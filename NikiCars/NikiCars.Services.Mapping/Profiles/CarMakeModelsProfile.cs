using AutoMapper;
using NikiCars.Data.Models;
using NikiCars.Models.CarMakeModels;

namespace NikiCars.Services.Mapping.Profiles
{
    public class CarMakeModelsProfile : Profile
    {
        public CarMakeModelsProfile()
        {
            CreateMap<CreateCarMakeModel, CarModel>(MemberList.None);
            CreateMap<ListCarMakeModel, CarMake>(MemberList.None).ReverseMap();
            CreateMap<EditCarMakeModel, CarMake>(MemberList.None);
        }
    }
}
