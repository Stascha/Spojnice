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
    /// Defined route path
    [Route("api/game")] // putanja dome:port/api/game
    [ApiController]
    /// GamesControler class
    public class GamesControler : ControllerBase
    {
        /// Our game repository
        private readonly IGameRepo _repository;
        /// mapper that is used to map source to the target/destination
        private  readonly IMapper _mapper;

        /// GamesControler Constructor
        public GamesControler(IGameRepo repository, IMapper mapper)
        {
            _repository = repository;            
            _mapper = mapper;
        }
        /** ### Desctiption
        *  Function that will return random Game if any game is in our database
        * ### Arguments
        * None.
        * ### Return value
        * ActionResult<GameReadDto> - returns positive GameReadDto object in case the game exist in the database and NotFound header in case no game is found in the database */
        [HttpGet] // // putanja dome:port/api/game GET
        public ActionResult<GameReadDto> GetRandomGame() //
        {
            var commandItem = _repository.GetRandomGame();
            if(commandItem != null){ 
               // ukoliko je uzeta igra mapira za izlazne podatke i salje OK, ili status code 200 sa podacima
                return Ok(_mapper.Map<GameReadDto>(commandItem));
            }
            // ako je commandItem ==null znaci da nemamo ni jednu igru unetu u bazi
            return NotFound();
        }

        /** ### Desctiption
        *  CreateGame method that is used to add new game to the Game table in our database.
        * ### Arguments
        * GameCreateDto gameCreateDto - object containing game data to insert into our database </br>
        * ### Return value
        * ActionResult <GameReadDto> - returns the object that is inserted in the database. */
        [HttpPost] // putanja dome:port/api/game POST
        public ActionResult <GameReadDto> CreateGame(GameCreateDto gameCreateDto)
        {
            var gameModel = _mapper.Map<GameModel>(gameCreateDto);
            _repository.CreateGame(gameModel);
            _repository.SaveChanges();
            var commandReadDto = _mapper.Map<GameReadDto>(gameModel);

            //grab all users : notifications = true
            IEnumerable<UserModel> userListToSendNotifications =  _repository.GetAllUsersWhereNotificationsIsTrue();
                new UserEmailSender().SendNotificationNewGameCreatedMail(userListToSendNotifications, gameCreateDto.Name);
            return Ok(commandReadDto); // vraca uneti podatak
        }

        /** ### Desctiption
        *  UpdateGame - function that is going to update the game in our database
        * ### Arguments
        * GameUpdateDto gameUpdateDto - object that contains the new data that will be placed on an existing object that is in the database. </br>
        * ### Return value
        * ActionResult - returns NoContent*/
        [HttpPut]
        public ActionResult UpdateGame(GameUpdateDto gameUpdateDto)
        {
            var gameModelFromRepo = _repository.GetGameById(gameUpdateDto.Id);
            if(gameModelFromRepo == null){ return NotFound();}

            _mapper.Map(gameUpdateDto, gameModelFromRepo);
            //_repository.UpdateGame(gameModelFromRepo);
            _repository.SaveChanges();
            //return NoContent();
            return NoContent();
        }

        /** ### Desctiption
        * Deletes the chosen game bt id from the database. Route from the root route is /{id}
        * ### Arguments
        * int id - id of the game in the database that will be removed </br>
        * ### Return value
        * ActionResult - returns NoContent on success and NotFound in case the set id is not found in the database*/
        [HttpDelete("{id}")]
        public ActionResult DeleteGame(int id){
            var gameModelFromRepo = _repository.GetGameById(id);
            if(gameModelFromRepo == null){ return NotFound();}

            _repository.DeleteGame(gameModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        /** ### Desctiption
        * Method that returns full list of Game data that is in our database Game table. 
        * Route from the root route is /all
        * ### Arguments
        * None.
        * ### Return value
        * ActionResult - returns Game list*/
        [HttpGet("all")] // // putanja dome:port/api/game GET
        public ActionResult<IEnumerable<GameReadDto>> GetAllGames()
        {
            var scoreItems = _repository.GetAllGames();
            return Ok(_mapper.Map<IEnumerable<GameReadDto>>(scoreItems));
        }
    }
}