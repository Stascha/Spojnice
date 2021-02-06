using System.Collections.Generic;
using Game.Models;
using User.Models;

namespace Game.Data
{
    /// Interface that SqlGameRepo is going to implmenet and thus will need to have all the methods described.
    public interface IGameRepo
    {
        bool SaveChanges();
        GameModel GetRandomGame();
        GameModel GetGameById(int id);
        void CreateGame(GameModel gm);
        void UpdateGame(GameModel gm);

        void DeleteGame(GameModel gm);
        IEnumerable<GameModel> GetAllGames();
        IEnumerable<UserModel> GetAllUsersWhereNotificationsIsTrue();

    }
}