using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Score.Data;
using Score.Dtos;
using Score.Models;
using User.Data;
using User.Models;

namespace Score.Controllers
{
    /// Defined route path
    /// ScoreControler class
    [Route("api/score")]
    [ApiController]
    public class ScoreControler : ControllerBase
    {
        /// Our game repository
        private readonly IScoreRepo _repository;

        /// mapper that is used to map source to the target/destination
        private  readonly IMapper _mapper;

        /// GamesControler Constructor
        public ScoreControler(IScoreRepo repository, IMapper mapper)
        {
            _repository = repository;            
            _mapper = mapper;
        }

        /** ### Desctiption
        * Function that is going to return Score List.
        * ### Arguments
        * None.
        * ### Return value
        * ActionResult<IEnumerable<ScoreReadDto>> - ScoreReadDto List */
        [HttpGet]
        public ActionResult<IEnumerable<ScoreReadDto>> GetAllScores()
        {
            var scoreItems = _repository.GetAllScores();
            return Ok(_mapper.Map<IEnumerable<ScoreReadDto>>(scoreItems));
        }

        /** ### Desctiption
        * CreateScore - creates new score in the database, or updates an old one.
        * If inserted score provided has highest score, user that was at the first place will receive notification email
        * in case the user has enabled notifications - if not no email will be sent out.
        * ### Arguments
        * ScoreCreateDto scoreCreateDto - score object.
        * ### Return value
        * ActionResult<IEnumerable<ScoreReadDto>> - ScoreReadDto List */
        [HttpPost]
        public ActionResult <ScoreReadDto> CreateScore(ScoreCreateDto scoreCreateDto)
        {
            var scoreItem = _repository.GetScoreByUsername(scoreCreateDto.Username);
            ScoreModel firstBefore = _repository.GetCurrentFirstScore();
            
            if(scoreItem != null){ // ako postoji zapis u bazi
            scoreItem.Score = scoreCreateDto.Score;
            } else{
             var scoreModel = _mapper.Map<ScoreModel>(scoreCreateDto);
            _repository.CreateScore(scoreModel);
            }
            _repository.SaveChanges();

// Sledeci deo koda salje email ako igrac nije vise prvi na tabeli
            ScoreModel firstAfter = _repository.GetCurrentFirstScore();
            if(firstBefore.Username != firstAfter.Username && firstBefore != null && firstAfter != null){
                UserModel beforeFirstUser = _repository.GetUserByUsername(firstBefore.Username);
                if(beforeFirstUser != null){
                    if(beforeFirstUser.Notifications == true){
                        new UserEmailSender().SendNoLongerFirstMail(beforeFirstUser, firstAfter.Username);
//////////////////////////////////////
                    }
                }
            }
            //GET now First If inserted is first, send mail to other one
            return Ok(); 
        }

    }
}