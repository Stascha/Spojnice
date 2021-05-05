using AutoMapper;
using User.Dtos;
using User.Models;

namespace User.Profiles
{
    /// UsersProfile klasa
    public class UsersProfile : Profile
    {
        /// UsersProfile konstrktor gde se mapira izvor na cilj/odrediste
        public UsersProfile()
        {
            // mapping source -> target / destination
            CreateMap<UserModel, UserReadDto>();
            CreateMap<UserCreateDto, UserModel>();
        }
    }
}