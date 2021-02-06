using System;
using System.Collections.Generic;
using System.Linq;
using Game.Models;
using User.Data;
using User.Models;

namespace Game.Data
{
    /// SQL Game Repository class
    public class SqlGameRepo : IGameRepo
    {
        /// _context contains our database context.
        private readonly GameContext _context;
        /// Constructor of SqlGameRepo Class
        public SqlGameRepo(GameContext context)
        {
            _context = context;
        }
        /** ### Description
        * CreateGame method - creates new game from the GameModel object received. Throws error if GameModel object is not provided.
        * ### Arguments
        * GameModel gm - game to add <br>
        * ### Return value
        * None.*/
        public void CreateGame(GameModel gm)
        {
            if (gm==null){
                throw new ArgumentNullException(nameof(gm));
            }
            _context.Games.Add(gm); // radi SQL insert 
        }
        /** ### Description
        * GetRandomGame method - chooses random Game from the database and returns it.
        * ### Arguments
        * None.
        * ### Return value
        * GameModel - random chosen.*/
        public GameModel GetRandomGame()
        {
            return _context.Games.OrderBy(c => Guid.NewGuid()).FirstOrDefault();
            //uzima random elemenat iz nase liste, sortira po ovom GUID koji ce uvek sortirati drugacije
        }
        /** ### Description
        * SaveChanges method - saves changes that are made to our _context.
        * ### Arguments
        * None.
        * ### Return
        * (_context.SaveChanges() >= 0);*/
        public bool SaveChanges()
        {
            // potreban da bi cuvali promene nakon da uradimo npr INSERT podataka u nasu bazu
            return (_context.SaveChanges() >= 0);
        }
        /** ### Description
        * UpdateGame method - function placeholder, is not implemented at the moment since we can update the Game Easily by maooing.
        * ### Arguments
        * None.
        * ### Return value
        * Throws NotImplementedException Exception .*/
        public void UpdateGame(GameModel gm) 
        {
            throw new NotImplementedException();
        }
        /** ### Description
        * DeleteGame method - Deletes the chosen game from the database
        * ### Arguments
        * GameModel gm - which object to delete </br>
        * ### Return value
        * None. */
        public void DeleteGame(GameModel gm) 
        {
            if(gm == null){
                throw new ArgumentNullException(nameof(gm));
            }
            _context.Remove(gm);
        }
        /** ### Description
        * GetAllGames method - Returns the list of all Games that are in the database
        * ### Arguments
        * None.
        * ### Return value
        * IEnumerable<GameModel> - Games List */
        public IEnumerable<GameModel> GetAllGames() 
        {
            return _context.Games.ToList();
        }

        /** ### Description
        * GetGameById method - Returns the object by provided ID
        * ### Arguments
        * int id - id of the Game that we want to be returned.</br>
        * ### Return value
        * GameModel - null if object does not exist. */
        public GameModel GetGameById(int id) 
        {
            return _context.Games.FirstOrDefault(p => p.Id == id);
        }

        /** ### Description
        * GetAllUsersWhereNotificationsIsTrue method - Returns the list of objects that have notifications enabled.
        * ### Arguments
        * None.
        * ### Return value
        * IEnumerable<UserModel> - List of users that have enabled notifactions. */
        public IEnumerable<UserModel> GetAllUsersWhereNotificationsIsTrue(){
            return _context.Users.Where(x=>x.Notifications == true);
        }
    }
}