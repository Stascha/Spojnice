using AutoMapper;
using Game.Dtos;
using Game.Models;

namespace Game.Profiles
{
    /// GameProfile class
    public class GameProfile : Profile
    {
        /// GameProfile constructor where we are mapping source to the target / destination
        public GameProfile()
        {
            CreateMap<GameModel, GameReadDto>();
            CreateMap<GameCreateDto, GameModel>();
            CreateMap<GameUpdateDto, GameModel>();
        }
    }
}