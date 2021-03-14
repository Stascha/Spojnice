using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using User.Data;
using User.Dtos;
using User.Models;
using System.Linq;

namespace User.Controllers
{
    /// Adjunctive class containing log in informations of the user
    public class UserRequestLogiObject{

        public string username { get; set; }
        public string password { get; set; }
       
    }

    /// Defined route path
    /// GamesControler class
    [Route("api/users")]
    [ApiController]
    public class UsersControler : ControllerBase
    {
         /// Our game repository
        private readonly IUserRepo _repository;

        /// mapper that is used to map source to the target/destination
        private  readonly IMapper _mapper;

        /// GamesControler Constructor
        public UsersControler(IUserRepo repository, IMapper mapper)
        {
            _repository = repository;            
            _mapper = mapper;
        }

        /** ### Desctiption
        *  Function that will check if entered creditionals (username and password) match with what database has.
        * If they match user object is going to be returned.
        * ### Arguments
        * UserRequestLogiObject inputData - contains username and password fields as strings.
        * ### Return value
        * ActionResult<UserReadDto> -  returns UserReadDto object in case the user exist in the database
        * and NotFound response in case no user is found in the database */
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
        * Function that will create new user with the provided data.
        * It is going to generate new notifications token and assugn it as false at the beginning.
        * ### Arguments
        *UserCreateDto usrCreateDto - contains fields for the user that are described in the Dto class.
        * ### Return value
        * ActionResult<UserReadDto> -  returns UserReadDto object in case the user is created
        * and USER_EXISTS string as the response in case if the user exists in the database */
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
        * Function that will change the status of notifications for the user.
        * ### Arguments
        * string notificationToken - notification token that is unique to each user. </br>
        * bool status - if user wants to enable notifications or not. </br>
        * ### Return value
        * ActionResult <string> - string that says the notifications is set to be true or false */
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