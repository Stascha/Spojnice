using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using User.Data;
using User.Dtos;
using User.Models;
using System.Linq;

namespace User.Controllers
{
    /// Dodatna klasa koja sadrzi informacije o igracu sa kojima se igrac loguje
    public class UserRequestLogiObject{

        public string username { get; set; }
        public string password { get; set; }
       
    }

    /// Definisana putanja rute
    /// GamesControler klasa
    [Route("api/users")]
    [ApiController]
    public class UsersControler : ControllerBase
    {
         /// Nas game repositori
        private readonly IUserRepo _repository;

        /// Maper koji se koristi za mapiranje izvora na cilj/odrediste
        private  readonly IMapper _mapper;

        /// GamesControler Konstruktor
        public UsersControler(IUserRepo repository, IMapper mapper)
        {
            _repository = repository;            
            _mapper = mapper;
        }

        /** ### Desctiption
        * Funkcija proverava da li uneto korisnicko ime i lozinka postoje u bazi podataka
        * Ako postoje objekat sa tim korisnickim imenom i lozinkom ce biti vracen.
        * ### Arguments
        * UserRequestLogiObject inputData - Korisnicko ime i lozinka.
        * ### Return value
        * ActionResult<UserReadDto> -  returns UserReadDto objekat ako taj igrac postoji u bazi podataka
        * ili NotFound ako taj igrac nije pronadjen u bazi podataka */
        [HttpPost("/login")] 
        public ActionResult<UserReadDto> GetUserByUsernameAndPassword(UserRequestLogiObject inputData) //  ActionResult<Command>
        {
            var username = inputData.username;
            var pwd = inputData.password;
            var usrItem = _repository.GetUserByUsernameAndPassword(username, pwd);
            if(usrItem != null){
 
                return Ok(_mapper.Map<UserReadDto>(usrItem));
            }
            return NotFound();
        }

        /** ### Desctiption
        * Funkcija koja kreira novog igraca sa prosledjenim podacima.
        * Kreira novi notifications token i postavlja mu vrednost false u pocetku
        * ### Arguments
        *UserCreateDto - sadrzi polja za igraca koja su opisana u Dto klasi.
        * ### Return value
        * ActionResult<UserReadDto> -  vraca UserReadDto objekat ako kreira novog igraca
        * ili USER_EXISTS string ako taj igrac vec postoji u bazi podataka */
        [HttpPost("/create")]
        public ActionResult <UserReadDto> CreateCommand(UserCreateDto usrCreateDto)
        {
            UserModel userModel = new UserModel();
            userModel.Email = usrCreateDto.Email;
            userModel.Role = usrCreateDto.Role;
            userModel.Username = usrCreateDto.Username;
            //disable notifications at the beginning
            userModel.Notifications = false;
           
            //hash password
            userModel.Password = BCrypt.Net.BCrypt.HashPassword(usrCreateDto.Password);

            //generate random notification token
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            System.Random random = new System.Random();
            userModel.NotificationToken = new string(Enumerable.Repeat(chars, 20).Select(s => s[random.Next(s.Length)]).ToArray());
            
            var usrModel = _mapper.Map<UserModel>(userModel);
            var usrItem = _repository.GetUserByUsername(usrModel.Username);
            var emailItem = _repository.GetUserByEmail(usrModel.Email);
            
            if(usrItem == null && emailItem == null)
            {
                _repository.CreateUser(usrModel);
                _repository.SaveChanges();
                var usrReadDto = _mapper.Map<UserReadDto>(usrModel);
                new UserEmailSender().SendWelcomeMail(userModel);
             /*   try
                {
                    new UserEmailSender().SendWelcomeMail(userModel);
                }
                catch(Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
                */
               
                return Ok(usrReadDto); 
            }
            return Ok("USER_EXISTS");
        }

        /** ### Desctiption
        * Funkcija koje menja igracev status za dobijanje email poruka od aplikacije
        * ### Arguments
        * string notificationToken - notification token koji je jedinstven za svakog korisnika \n
        * bool status - da li korisnik zeli da omoguci dobijanje poruka od aplikacije ili ne zeli \n 
        * ### Return value
        * ActionResult <string> - string koji kaze da li su notifikacije odobrene ili ne */
        [HttpGet("notification/change/{notificationToken}/{status}")]
        public ActionResult <string> NotificationStatusChange(string notificationToken, bool status)
        {
            UserModel selectedUser = _repository.GetUserByNotificationToken(notificationToken);
            if(selectedUser == null){ return NotFound();}
            selectedUser.Notifications = status;
            _repository.SaveChanges();
            return Ok("Notifications set: " + status);
        }
    }
}