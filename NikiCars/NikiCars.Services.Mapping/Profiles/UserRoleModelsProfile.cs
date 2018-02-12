using AutoMapper;
using NikiCars.Data.Models;
using NikiCars.Models.UserRoleModels;

namespace NikiCars.Services.Mapping.Profiles
{
    public class UserRoleModelsProfile : Profile
    {
        public UserRoleModelsProfile()
        {
            CreateMap<CreateUserRoleModel, UserRole>(MemberList.None);
        }
    }
}
