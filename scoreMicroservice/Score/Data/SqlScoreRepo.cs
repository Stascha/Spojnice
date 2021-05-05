using System;
using System.Collections.Generic;
using System.Linq;
using Score.Models;
using User.Models;

namespace Score.Data
{
    /// SQL Repository klasa
    public class SqlScoreRepo : IScoreRepo
    {
        /// _context sadrzi sadrzaj baze podataka.
        private readonly ScoreContext _context;

        /// Konstruktor SqlScoreRepo Klase
        public SqlScoreRepo(ScoreContext context)
        {
            _context = context;
        }

        /** ### Description
        * CreateScore metod - kreira novi skor iz dobijenog ScoreModel objekta. \n 
        * Izbacuje error ako ScoreModel objekat nije prosledjen.
        * ### Arguments
        * ScoreModel score - skor koji se dodaje \n 
        * ### Return value
        * Nema.*/
        public void CreateScore(ScoreModel score)
        {
            if (score==null){
                throw new ArgumentNullException(nameof(score));
            }
            _context.Score.Add(score);
        }

        /** ### Description
        * GetAllScores metod - vraca sve skoreve.
        * ### Arguments
        * Nema.
        * ### Return value
        * IEnumerable<ScoreModel> - lista skoreva.  \n Ako objekat nije pronadjen u bazi podataka bice vracen null. */
        public IEnumerable<ScoreModel> GetAllScores()
        {
            return _context.Score.OrderByDescending(o=>o.Score).ToList();
        }

        /** ### Description
        * GetScoreByUsername metod - vraca skor za prosledjeno korisnicko ime.
        * ### Arguments
        * string usrname - user korisnicko ime \n 
        * ### Return value
        * ScoreModel - score objekat. \n Ako objekat nije pronadjen u bazi podataka bice vracen null.*/
        public ScoreModel GetScoreByUsername(string usrname)
        {
            return _context.Score.FirstOrDefault(p => p.Username == usrname);
        }

        /** ### Description
        * RemoveScore method - brise skor
        * ### Arguments
        * ScoreModel score - score objekat koji treba da bude izbrisan \n 
        * ### Return value
        * Nema.*/
        public void RemoveScore(ScoreModel score)
        {
            _context.Score.Remove(score);
        }

        /** ### Description
        * GetUserByUsername method - vraca user objekat na osnovu prosledjenog korisnickog imena.
        * ### Arguments
        * string usrname - user korisnicko ime </br>
        * ### Return value
        * UserModel - score objekat.  \n Ako objekat nije pronadjen u bazi podataka bice vracen null. */
        public UserModel GetUserByUsername(string usrname){
            return _context.Users.FirstOrDefault(x=>x.Username == usrname);
        }

        /** ### Description
        * GetCurrentFirstScore method - vraca najveci skor objekat.
        * ### Arguments
        * None.
        * ### Return value
        * ScoreModel - score objekat. \n Ako baza podataka nema objekte bice vrace null.*/
        public ScoreModel GetCurrentFirstScore(){
            return _context.Score.OrderByDescending(o=>o.Score).FirstOrDefault();
        }

        /** ### Description
        * SaveChanges method - cuva promene koje su napravljene u _context.
        * ### Arguments
        * Nema.
        * ### Return
        * (_context.SaveChanges() >= 0);*/
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}