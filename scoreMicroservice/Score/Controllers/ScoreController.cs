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
    /// Definisana putanja rute
    /// ScoreControler klasa
    [Route("api/score")]
    [ApiController]
    public class ScoreControler : ControllerBase
    {
        /// Game repository
        private readonly IScoreRepo _repository;

        /// Maper koji se koristi za mapiranje izvora na cilj/odrediste
        private readonly IMapper _mapper;

        /// ScoreControler Konstruktor
        public ScoreControler(IScoreRepo repository, IMapper mapper)
        {
            _repository = repository;            
            _mapper = mapper;
        }

        /** ### Desctiption
        * Function koja vraca Skor Listu.
        * ### Arguments
        * Nema.
        * ### Return value
        * ActionResult<IEnumerable<ScoreReadDto>> - ScoreReadDto Lista */
        [HttpGet]
        public ActionResult<IEnumerable<ScoreReadDto>> GetAllScores()
        {
            var scoreItems = _repository.GetAllScores();
            return Ok(_mapper.Map<IEnumerable<ScoreReadDto>>(scoreItems));
        }

        /** ### Desctiption
        * CreateScore - kreira novi skor u bazi podataka ili menja stari skor.
        * Ako je kreirani skor najvisi, igracu koji je do tada bio na prvom mestu 
        * na skor tabeli ce biti poslat notifikacioni email da se vise ne nalazi na prvom mestu
        * u slucaju da je taj igrac omogucio dobijanje notifikacija od aplikacije a ako nije omogucio dobijanje 
        * notifikacija od aplikacije nece mu biti poslat email.
        * ### Arguments
        * ScoreCreateDto scoreCreateDto - score objekat.
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
           
            return Ok(); 
        }

    }
}