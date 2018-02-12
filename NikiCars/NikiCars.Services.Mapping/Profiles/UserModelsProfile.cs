using AutoMapper;
using NikiCars.Data.Models;
using NikiCars.Models.UserModels;

namespace NikiCars.Services.Mapping.Profiles
{
    public class UserModelsProfile : Profile
    {
        public UserModelsProfile()
        {
            CreateMap<EditUserModel, User>(MemberList.None).ReverseMap();
            CreateMap<FindUserModel, User>(MemberList.None).ReverseMap();
        }
    }
}
