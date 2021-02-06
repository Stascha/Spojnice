using System.Collections.Generic;
using Score.Models;
using User.Models;

namespace Score.Data
{
    /// Interface that SqlUserRepo is going to implmenet and thus will need to have all the methods described.
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