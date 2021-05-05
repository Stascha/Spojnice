import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GameService } from 'src/app/services/game.service';
import { UserService } from 'src/app/services/user.service';

/** JS funkcija definisana u index.html fajlu.
 * Funkcija koja prikazuje modal sa tekstom igracu. */
declare const sendMessageUserModal: any; // funkcija koja je definisana u INDEX.HTML fajlu dole u script tagu
// ovako je pozivamo i kazemo joj sta i gde da izvrsi

/**
 * Component - LoginComponent
 */
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
/** Klasa koja se koristi za stranu za logovanje. */
export class LoginComponent implements OnInit {

   /** Povezuje se usr sa username poljem za unos */
    usr: string = ''; // radimo binding sa value on input text field-a

   /** Povezuje se pwd sa password poljem za unos */
    pwd: string = '';// radimo binding sa value on input text field-a

   /** Povezuje se eml sa email poljem za unos. To pokazuje da li je igrac izabrao opciju za registraciju */
    eml: string = '';
    showUserLoginForm : boolean = true;
  // Povezivanje nam omogucava da pristupom na promenjljivu dobijamo sta je u tekstualnim poljima u realnom vremenu
  /**
   * Proverava da li UserService ima podatke o igracu, to znači da je igrac ulogovan.
   * Ako je igrac ulogovan on ce biti redirektovan na stranicu za igru.
   * @param {UserService} userService
   * @param {Router} router 
   * @param {GameService} gameService 
   */
  constructor(private userService : UserService, private router:Router, private gameService: GameService) {
    //private userService : UserService - mozemo pristupiti userService varijabli kao i njenim metodama
    //, private router:Router - pristupamo ruteru
    if(userService.getLoggedinUser().id != -1){
      // ako nije ulogovan radi redirect na /login stranu
      // -1 je defaultni i za logout kada nema user-a ulogovanog
      this.router.navigateByUrl('/', {skipLocationChange: true}).then(()=>
      this.router.navigate(["/game"]));
    }
    else {
      this.gameService.gameActive = false;
    }
   }
  /** Prazan metod, ne radi nista. */
  ngOnInit(): void {
  }
/**
 * Metod se poziva kad igrac klikne na dugme Logovanje
 */
  onLogin(): void {
    // subscribe - zato sto je sve asinhrono
    // cekamo odgovor od servera kada server vrati dobar kod, npr 200 mi dobijene podatke dobijamo u data
    // ako vrati neki error ili resurs nije mogo biti nadjen/nema odgovora od servera to ide u error

      this.userService.loginUser(this.usr, this.pwd).subscribe((data) => {
          console.log('[LOGIN USER RES]: ', data)
          this.onApiLoginOrRegisterActionResult(data)
      }, (err) => { this.onApiLoginOrRegisterActionResult("lozinka ili korisnicko ime netacno!") });
     
 }

/**
 * Metod se poziva kad igrac klikne na dugme Registracija
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
                (err) => {
                    this.onApiLoginOrRegisterActionResult("korisnicko ime je zauzeto!")
                    //console.log(err)
                }
            );
        
        }
    
    }
/**
 * Ako je input parametar Objekat ova funkcija ce postaviti aktivnog korisnika - igraca u UserService.
 * Ako je input parametar tekst tada ce se pozvati funkcija koja ce prikazati panel sa porukom igracu.
 * @param res {any} Predstavlja rezultat koji treba da dobijemo od servera. To moze biti Objekat ili tekst   
 */
  onApiLoginOrRegisterActionResult(res:any){
    if(res instanceof Object)
    {
      // Proveravamo da li je res javascript objekat ako jeste postavljamo ulogovanog igraca
      this.userService.setActiveUser({           
        id         : res.id,       
        username   : res.username,    
        email      : res.email,
        role       : res.role,
        notificationsToken: res.notificationToken,
        notificationsActive:res.notifications,
      });


        //radimo redirect na game komponentu this.router.navigateByUrl('/', { skipLocationChange: true }).then(() =>
      this.router.navigate(["/game"]);//);
    }
    else {
      sendMessageUserModal(res); // ako nije objekat salje da ispise sve preko modala
    }
  }

}
