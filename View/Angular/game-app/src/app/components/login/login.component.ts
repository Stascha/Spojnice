import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GameService } from 'src/app/services/game.service';
import { UserService } from 'src/app/services/user.service';
/** JS function defined in the index.html file. Function that will show the modal to the end user with the text as input. */
declare const sendMessageUserModal: any; // funkcija koja se nalazu u INDEX.HTML fajlu dole u script tagu
// ovako je pozivamo i kazemo joj sta i gde da izvrsi

/**
 * Component - LoginComponent
 */
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
/** Class that is used for our login page. */
export class LoginComponent implements OnInit {
  /** We are binding usr to the username input field */
    usr: string = ''; // radimo binding sa value on input text field-a
    /** We are binding pwd to the password input field */
    pwd: string = '';// radimo binding sa value on input text field-a
    /** We are binding eml to the email input field that is shown if users chooses the register option */
    eml: string = '';
    showUserLoginForm : boolean = true;
  // binding nam omogucava da pristupom na varijablu dobijamo sta je u tekstualnim poljima u "real time"
  /**
   * Checks if the UserService has data about the user, this means that user is logged. If the user is loggedin, the user will be redirected to the /game route.
   * @param {UserService} userService
   * @param {Router} router 
   * @param {GameService} gameService 
   */
  constructor(private userService : UserService, private router:Router, private gameService: GameService) {
    //private userService : UserService - mozemo pristupiti userService varijabli kao i njenim metodama
    //, private router:Router - pristupamo ruteru
    if(userService.getLoggedinUser().id != -1){
      // ako nije logovan radi reddirect na /login stranu
      // -1 smo stavili kao defaultni i za logout kada nema user-a logovanog
      this.router.navigateByUrl('/', {skipLocationChange: true}).then(()=>
      this.router.navigate(["/game"]));
    }
    else {
      this.gameService.gameActive = false;
    }
   }
  /** Empty method, does not do anything. */
  ngOnInit(): void {
  }
/**
 * This method is called when User clicks on the Login Button
 */
  onLogin(): void {
    //subscribe - zato sto je sve asinhrono
    // cekamo odgovor od servera kada server vrati dobar kod, npr 200 mi dobijene podatke dobijamo u data
    // ako vrati neki error ili resurs nije mogo biti nadjen/nema odgovora od servera to ide u error

      this.userService.loginUser(this.usr, this.pwd).subscribe((data) => {
          console.log('[LOGIN USER RES]: ', data)
          this.onApiLoginOrRegisterActionResult(data)
      }, (err) => { this.onApiLoginOrRegisterActionResult("lozinka ili korisnicko ime netacno!") });
     
 }

/**
 * This method is called when User clicks on the Register
 */
    onRegister(): void {

        if (this.usr == "")
            this.onApiLoginOrRegisterActionResult("Unesite neki Username");
        
        else if (this.pwd == "") 
            this.onApiLoginOrRegisterActionResult("Unesite neki Pasword");

        else if (this.eml == "")
            this.onApiLoginOrRegisterActionResult("Unesite email adresu");

        else {
            this.userService.createNewUser(this.usr, this.pwd, this.eml, "user").subscribe(
                (data) => { this.onApiLoginOrRegisterActionResult(data) },
                (err) => { this.onApiLoginOrRegisterActionResult("korisnicko ime je zauzeto!") }
            );
        
        }
    
    }
/**
 * If the input param is an Object this function will set the active user in the UserService.
 * If the input param is text it will call the function that will trigger and open a dialog with a message to the end user.
 * @param res {any} represents the result that we should get from the server. It can be either Object type or text type
 */
  onApiLoginOrRegisterActionResult(res:any){
    if(res instanceof Object)
    {
      // proveravamo da li je res javascript objekat ako jeste postavljamo logovanog usera
      this.userService.setActiveUser({           
        id         : res.id,       
        username   : res.username,    
        email      : res.email,
        role       : res.role,
      });

    //radimo reddirect na game komponentu this.router.navigateByUrl('/', {skipLocationChange: true}).then(()=>
      this.router.navigate(["/game"]);//);
    }
    else {
      sendMessageUserModal(res); // ako nije objekat salji da ispise sve preko modala
    }
  }

}
