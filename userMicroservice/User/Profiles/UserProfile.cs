using AutoMapper;
using User.Dtos;
using User.Models;

namespace User.Profiles
{
    /// GameProfile class
    public class UsersProfile : Profile
    {
        /// UsersProfile constructor where we are mapping source to the target / destination
        public UsersProfile()
        {
            // mapping source -> target / destination
            CreateMap<UserModel, UserReadDto>();
            CreateMap<UserCreateDto, UserModel>();
        }
    }
}