using AutoMapper;
using NikiCars.Data.Models;
using NikiCars.Models.LoginModels;

namespace NikiCars.Services.Mapping.Profiles
{
    public class LoginModelsProfile : Profile
    {
        public LoginModelsProfile()
        {
            CreateMap<LoginModel, User>(MemberList.None);
        }
    }
}
