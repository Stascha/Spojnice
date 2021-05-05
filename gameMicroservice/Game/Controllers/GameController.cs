using AutoMapper;
using Game.Data;
using Game.Dtos;
using Game.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using User.Data;
using User.Models;

namespace Game.Controllers
{
    [Route("api/game")] // putanja dome:port/api/game
    [ApiController]

    /// GamesControler klasa
    public class GamesControler : ControllerBase
    {
        /// Game repository
        private readonly IGameRepo _repository;
        /// Maper koji se koristi za mapiranje izvora na cilj/odrediste
        private readonly IMapper _mapper;

        /// GamesControler Konstruktor
        public GamesControler(IGameRepo repository, IMapper mapper)
        {
            _repository = repository;            
            _mapper = mapper;
        }
        /** ### Desctiption
        * Funkcija koja vraca slucajno izabranu igru ako postoji neka igra u bazi podataka 
        * ### Arguments
        * Nema.
        * ### Return value
        * ActionResult<GameReadDto> - vraca GameReadDto objekat u slucaju da neka igra postoji u bazi podataka ili NotFound u slucaju da ni jedna igra nije pronadjena u bazi podataka */
        [HttpGet] // // putanja dome:port/api/game GET
        public ActionResult<GameReadDto> GetRandomGame() //
        {
            var commandItem = _repository.GetRandomGame();
            if(commandItem != null){ 
               // ukoliko je uzeta igra mapira za izlazne podatke i salje OK, ili status code 200 sa podacima
                return Ok(_mapper.Map<GameReadDto>(commandItem));
            }
            // ako je commandItem == null znaci da nemamo ni jednu igru unetu u bazi
            return NotFound();
        }

        /** ### Desctiption
        * CreateGame metod dodaje novu igru u tabelu Game u bazi podataka.
        * ### Arguments
        * GameCreateDto gameCreateDto - objekat sadrzi podatke za igru koja treba da se ubaci u bazu podataka \n 
        * ### Return value
        * ActionResult <GameReadDto> - vraca objekat koji je ubacen u bazu podataka*/
        [HttpPost] // putanja dome:port/api/game POST
        public ActionResult <GameReadDto> CreateGame(GameCreateDto gameCreateDto)
        {
            var gameModel = _mapper.Map<GameModel>(gameCreateDto);
            _repository.CreateGame(gameModel);
            _repository.SaveChanges();
            var commandReadDto = _mapper.Map<GameReadDto>(gameModel);

            // Uzimaju se svi igraci kod kojih je : notifications = true
            IEnumerable<UserModel> userListToSendNotifications =  _repository.GetAllUsersWhereNotificationsIsTrue();
            // Salje se poruka svim igracima, koji su omogucili notifikacije od aplikacije, da je kreirana nova igra.
                new UserEmailSender().SendNotificationNewGameCreatedMail(userListToSendNotifications, gameCreateDto.Name);
            return Ok(commandReadDto); 
        }

        /** ### Desctiption
        * UpdateGame - Funkcija koja menja postojecu igru u bazi podataka 
        * ### Arguments
        * GameUpdateDto gameUpdateDto - objekat koji sadrzi nove podatke koji ce biti promenjeni u postojecoj igri. \n 
        * ### Return value
        * ActionResult - vraca NoContent*/
        [HttpPut]
        public ActionResult UpdateGame(GameUpdateDto gameUpdateDto)
        {
            var gameModelFromRepo = _repository.GetGameById(gameUpdateDto.Id);
            if(gameModelFromRepo == null){ return NotFound();}

            _mapper.Map(gameUpdateDto, gameModelFromRepo);
            _repository.SaveChanges();
           
            return NoContent();
        }

        /** ### Desctiption
        * Brise igru na osnovu prosledjenog id-a igre iz baze podataka.
        * ### Arguments
        * int id - id igre koja treba da se izbrise iz baze podataka \n 
        * ### Return value
        * ActionResult - returns NoContent ako je igra izbrisana ili \n  
        * NotFound ako prosledjeni id igre ne postoji u bazi podataka */
        [HttpDelete("{id}")]
        public ActionResult DeleteGame(int id){
            var gameModelFromRepo = _repository.GetGameById(id);
            if(gameModelFromRepo == null){ return NotFound();}

            _repository.DeleteGame(gameModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        /** ### Desctiption
        * Metod koji vraca listu svih igara koje se nalaze u bazi podataka u Game tabeli. 
        * ### Arguments
        * Nema.
        * ### Return value
        * ActionResult - vraca listu svih igara*/
        [HttpGet("all")] // // putanja dome:port/api/game GET
        public ActionResult<IEnumerable<GameReadDto>> GetAllGames()
        {
            var scoreItems = _repository.GetAllGames();
            return Ok(_mapper.Map<IEnumerable<GameReadDto>>(scoreItems));
        }
    }
}