using AutoMapper;
using Game.Dtos;
using Game.Models;

namespace Game.Profiles
{
    /// GameProfile klasa
    public class GameProfile : Profile
    {
        /// GameProfile konstruktor gde se mapira izvor na cilj/odrediste
        public GameProfile()
        {
            CreateMap<GameModel, GameReadDto>();
            CreateMap<GameCreateDto, GameModel>();
            CreateMap<GameUpdateDto, GameModel>();
        }
    }
}