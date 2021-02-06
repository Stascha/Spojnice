using System;
using System.Collections.Generic;
using System.Linq;
using Score.Models;
using User.Models;

namespace Score.Data
{
    /// SQL Game Repository class
    public class SqlScoreRepo : IScoreRepo
    {
        /// _context contains our database context.
        private readonly ScoreContext _context;

        /// Constructor of SqlGameRepo Class
        public SqlScoreRepo(ScoreContext context)
        {
            _context = context;
        }

        /** ### Description
        * CreateScore method - creates new score from the ScoreModel object received.
        * Throws error if ScoreModel object is not provided.
        * ### Arguments
        * ScoreModel score - score to add </br>
        * ### Return value
        * None.*/
        public void CreateScore(ScoreModel score)
        {
            if (score==null){
                throw new ArgumentNullException(nameof(score));
            }
            _context.Score.Add(score);
        }

        /** ### Description
        * GetAllScores method - returs all Score records.
        * ### Arguments
        * None.
        * ### Return value
        * IEnumerable<ScoreModel> - list of scores.  If objects are not found in the database null is returned. */
        public IEnumerable<ScoreModel> GetAllScores()
        {
            return _context.Score.OrderByDescending(o=>o.Score).ToList();
        }

        /** ### Description
        * GetScoreByUsername method - returns score by username as input.
        * ### Arguments
        * string usrname - user username </br>
        * ### Return value
        * ScoreModel - score object. If object is not found in the database null is returned.*/
        public ScoreModel GetScoreByUsername(string usrname)
        {
            return _context.Score.FirstOrDefault(p => p.Username == usrname);
        }

        /** ### Description
        * RemoveScore method - removes score
        * ### Arguments
        * ScoreModel score - score object to remove/delete</br>
        * ### Return value
        * None.*/
        public void RemoveScore(ScoreModel score)
        {
            _context.Score.Remove(score);
        }

        /** ### Description
        * GetUserByUsername method - returns user object by username as input.
        * ### Arguments
        * string usrname - user username </br>
        * ### Return value
        * UserModel - score object. If object is not found in the database null is returned.*/
        public UserModel GetUserByUsername(string usrname){
            return _context.Users.FirstOrDefault(x=>x.Username == usrname);
        }

        /** ### Description
        * GetCurrentFirstScore method - returns highest score object.
        * ### Arguments
        * None.
        * ### Return value
        * ScoreModel - score object. If database has no objects, null is returned.*/
        public ScoreModel GetCurrentFirstScore(){
            return _context.Score.OrderByDescending(o=>o.Score).FirstOrDefault();
        }

        /** ### Description
        * SaveChanges method - saves changes that are made to our _context.
        * ### Arguments
        * None.
        * ### Return
        * (_context.SaveChanges() >= 0);*/
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}