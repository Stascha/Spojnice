using AutoMapper;
using Score.Dtos;
using Score.Models;

namespace Score.Profiles
{
    /// ScoreProfile class
    public class ScoreProfile : Profile
    {
        /// ScoreProfile constructor where we are mapping source to the target / destination
        public ScoreProfile()
        {
            // mapping source -> target / destination
            CreateMap<ScoreModel, ScoreReadDto>();
            CreateMap<ScoreCreateDto, ScoreModel>();
        }
    }
}