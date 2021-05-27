using System;
using System.Collections.Generic;
using System.Linq;
using Game.Models;
using User.Data;
using User.Models;

namespace Game.Data
{
    /// SQL Game Repository klasa
    public class SqlGameRepo : IGameRepo
    {
        /// _context sadrzi sadrzaj baze podataka.
        private readonly GameContext _context;

        /// Konstruktor SqlGameRepo klase
        public SqlGameRepo(GameContext context)
        {
            _context = context;
        }

        /** ### Description
        * CreateGame metod - kreira novu igru iz prosledjenog GameModel objekta. \n 
        * Izbacuje error ako GameModel objekat nije prosledjen.
        * ### Arguments
        * GameModel gm - igra koja se dodaje <br>
        * ### Return value
        * Nema.*/
        public void CreateGame(GameModel gm)
        {
            if (gm == null){
                throw new ArgumentNullException(nameof(gm));
            }
            _context.Games.Add(gm); // radi SQL insert 
        }

        /** ### Description
        * GetRandomGame method - Slucajnim izborom odabere igru iz baze podataka i vrati je.
        * ### Arguments
        * Nema.
        * ### Return value
        * GameModel - Slucajno izabrana igra. */
        public GameModel GetRandomGame()
        {
            return _context.Games.OrderBy(c => Guid.NewGuid()).FirstOrDefault();
            //uzima random elemenat iz nase liste, sortira po ovom GUID koji ce uvek sortirati drugacije
        }

        /** ### Description
        * SaveChanges method - cuva promene koje su napravljene u  _context.
        * ### Arguments
        * Nema.
        * ### Return
        * (_context.SaveChanges() >= 0);*/
        public bool SaveChanges()
        {
            // potreban da bi cuvali promene nakon da uradimo npr INSERT podataka u nasu bazu
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateGame(GameModel gm) 
        {
            throw new NotImplementedException();
        }
        

        /** ### Description
        * DeleteGame method - Brise izabranu igru iz baze podataka
        * ### Arguments
        * GameModel gm - objekat koji treba da se izbrise \n 
        * ### Return value
        * Nema. */
        public void DeleteGame(GameModel gm) 
        {
            if(gm == null){
                throw new ArgumentNullException(nameof(gm));
            }
            _context.Remove(gm);
        }

        /** ### Description
        * GetAllGames method - Vraca listu svih igara iz baze podataka
        * ### Arguments
        * Nema.
        * ### Return value
        * IEnumerable<GameModel> -  Lista igara */
        public IEnumerable<GameModel> GetAllGames() 
        {
            return _context.Games.ToList();
        }

        /** ### Description
        * GetGameById method - Vraca igru koja ima prosledjeni id
        * ### Arguments
        * int id - id igre koju ce da vrati. \n 
        * ### Return value
        * GameModel igru ili null ako igra sa prosledjenim id ne postoji. */
        public GameModel GetGameById(int id) 
        {
            return _context.Games.FirstOrDefault(p => p.Id == id);
        }

        /** ### Description
        * GetAllUsersWhereNotificationsIsTrue method - Vraca listu objekata igraca kod kojih je dobijanje notifikacija omoguceno.
        * ### Arguments
        * Nema.
        * ### Return value
        * IEnumerable<UserModel> - Lista igraca kod kojih je dobijanje notifikacija omuguceno. */
        public IEnumerable<UserModel> GetAllUsersWhereNotificationsIsTrue(){
            return _context.Users.Where(x=>x.Notifications == true);
        }
    }
}