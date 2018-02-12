using AutoMapper;
using NikiCars.Data.Models;
using NikiCars.Models.CarCoupeModels;

namespace NikiCars.Services.Mapping.Profiles
{
    internal class CarCoupeModelsProfile : Profile
    {
        public CarCoupeModelsProfile()
        {
            CreateMap<CreateCarCoupeModel, CarCoupe>(MemberList.None);
            CreateMap<EditCarCoupeModel, CarCoupe>(MemberList.None);
        }
    }
}
