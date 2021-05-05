using AutoMapper;
using Score.Dtos;
using Score.Models;

namespace Score.Profiles
{
    /// ScoreProfile klasa
    public class ScoreProfile : Profile
    {
        /// ScoreProfile konstrktor gde se mapira izvor na cilj/odrediste
        public ScoreProfile()
        {
            // mapping source -> target / destination
            CreateMap<ScoreModel, ScoreReadDto>();
            CreateMap<ScoreCreateDto, ScoreModel>();
        }
    }
}