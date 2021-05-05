using System.Collections.Generic;
using Game.Models;
using User.Models;

namespace Game.Data
{
    /// Interfejs koji SqlGameRepo klasa implementira i definise sve metode.
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