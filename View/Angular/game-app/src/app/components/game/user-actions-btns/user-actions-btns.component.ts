import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GameService } from 'src/app/services/game.service';
import { UserService } from 'src/app/services/user.service';
import { GameComponent } from '../game.component';

/** JS funkcija definisana u index.html fajlu. Funkcija koja inicijalizuje modal reference when called. */
declare const initializeSettingsModalVar: any;
/** JS funkcija definisana u index.html fajlu. Funkcija koja prikazuje modal igracu. */
declare const settingsModalShow: any;

/** Component - UserActionsBtnsComponent*/
@Component({
  selector: 'app-user-actions-btns',
  templateUrl: './user-actions-btns.component.html',
  styleUrls: ['./user-actions-btns.component.css']
})
/**
 * Contains action buttons - mainly the navigation for the user.
 */
export class UserActionsBtnsComponent implements OnInit {
  /**
   * Konstruktor
   * @param userService {UserService}
   * @param router {Router}
   * @param gameService {GameService}
   * @param gameComponent {GameComponent}
   */
  constructor(
    public userService : UserService,
    private router:Router,
    private gameService: GameService,
    private gameComponent : GameComponent
  ) {
    
  }
  /** Setting up the settings modal variable reference */
  ngOnInit(): void {
    initializeSettingsModalVar();
  }

  /**
   * Kada igrac odabere da hoce da se izloguje, tada ce metod logoutUser iz UserService klase da izloguje igraca.
   * Posle toga ce igrac biti redirektovan na login stranicu.
   */
  onUserLogout():void{
    this.userService.logoutUser();
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(()=>
    this.router.navigate(["/login"]));
  }

  /**
   * Nova igra ce biti zapoceta (via gameComponent) i igra ce biti postavljena da je aktivna (gameService - attribute: gameActive ).
   */
  onNewGame(){
    this.gameComponent.startTheGame();
    this.gameService.gameActive = true;
  }

  /**
   * Funkcija koja otvara modal za podesavanje dobijanja notifikacija od aplikacije
   */
  onSettingsModalOpen(){
    settingsModalShow(true);
  }

  /**
   * Funkcija koja zatvara modal za podesavanje dobijanja notifikacija od aplikacije
   * Kada se modal zatvori, vrednost koja je postavljena na modalu ce biti promenjana u bazi podataka u koloni Notifications.
   */
  onSettingsModalClose(){
    settingsModalShow(false);
  }

  /**
   * Funkcija koja menja trenutnu vrednost notifikacije u korisnickom objektu na frontendu i kontaktira funkciju iz
   * userService koja kontaktira microservice API da bi se promenila vrednost u koloni Notifications u bazi podataka.
   */
  onSettingsNotificationCheckBoxChange(){
    this.userService.getLoggedinUser().notificationsActive = !this.userService.getLoggedinUser().notificationsActive;
    this.userService.changeEmailNotifications().subscribe()
  }
}
