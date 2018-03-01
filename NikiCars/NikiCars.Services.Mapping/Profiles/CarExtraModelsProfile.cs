using AutoMapper;
using NikiCars.Data.Models;
using NikiCars.Models.CarExtraModels;

namespace NikiCars.Services.Mapping.Profiles
{
    public class CarExtraModelsProfile : Profile
    {
        public CarExtraModelsProfile()
        {
            CreateMap<CreateCarExtraModel, Extra>(MemberList.None);
            CreateMap<EditCarExtraModel, Extra>(MemberList.None);
            CreateMap<ListAllCarExtraModel, Extra>(MemberList.None);
        }
    }
}
