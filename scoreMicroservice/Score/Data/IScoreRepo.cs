using System.Collections.Generic;
using Score.Models;
using User.Models;

namespace Score.Data
{
    /// Interfejs koji SqlScoreRepo klasa implementira i definise sve metode.
    public interface IScoreRepo
    {
        bool SaveChanges();
        IEnumerable<ScoreModel> GetAllScores();
        void CreateScore(ScoreModel score);
        void RemoveScore(ScoreModel score);
        ScoreModel GetScoreByUsername(string usrname);
        UserModel GetUserByUsername(string usrname);
        ScoreModel GetCurrentFirstScore();

    }
}