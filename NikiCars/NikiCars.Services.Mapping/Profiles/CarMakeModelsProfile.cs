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
        }
    }
}
